using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Net.NetworkInformation;
using NS_OpenCV;
using svm.spatial;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET;

using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using System.Collections.Concurrent;

namespace ImageProcessingControl
{
    public partial class ImageProcessingDialog : Form
    {
        delegate void SetIsRealTaregtInCropListViewCallback(int index, string text);
        delegate void SetLonAndLatInListCropViewCallback(int index, string lat,string lon);
        delegate void AddTrueTargetCallBack(TargetImage target, Image smallTrueImage, Image largeTrueImage);



        //home coordinate
        private float HomeLat = float.Parse( System.Configuration.ConfigurationManager.AppSettings["homeLatitude"]);
        private float HomeLon = float.Parse(System.Configuration.ConfigurationManager.AppSettings["homeLongitude"]);
        private float HomeAlt = float.Parse(System.Configuration.ConfigurationManager.AppSettings["homeAltitude"]);

        public static double HomeLatGoogle = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["homeLatitude"]);
        public static double HomeLonGoogle = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["homeLongitude"]);

        //server properties
        private bool GetImages = false;
        private bool ImagesThreadRunning = false;
        private Thread ImageThread;
        //Stopwatch properties
        private Stopwatch stopWatch = new Stopwatch();
        private System.Windows.Forms.Timer timer;
        private bool timerRunning = false;
        //Ping properties
        private bool pingThreadRunning = false;
        private bool GetPing = false;
        //Mser properties
        private OpenCvCLR opencv;
        private bool mserThreadRunning = false;
        private int delta;
        private int maxVariation;
        private int minDiversity;
        private int minArea;
        private int maxArea;

        private bool copyThreadRunning = false;

        private bool classificationThreadRunning = false;
        private Thread classifyThraed;
        private Mutex classifyMut = new Mutex();

        private Mutex GPSMut = new Mutex();

        private ConcurrentDictionary<string, ImageFromServer> sourceImages = new ConcurrentDictionary<string, ImageFromServer>();
        private ConcurrentDictionary<string, TargetImage> crops = new ConcurrentDictionary<string, TargetImage>();
        private ConcurrentQueue<TargetImage> ToClassifyCrops = new ConcurrentQueue<TargetImage>();

        private GoogleMapDialog GoogleMapdlg;
        public BlockingCollection<PointLatLng> markers = new BlockingCollection<PointLatLng>();
        public static bool googleIsRunning = false;


        private int trueTargetCounter = 0;
        private bool IsResetClicked = false;

        private class Features
        {
            public PointLatLng location;
            public string letter;
            public string shape;

            public Features(PointLatLng p, string let, string shp)
            {
                location = p;
                letter = let;
                shape = shp;
            }
        }

        //private List<PointLatLng> coordList;
        private List<Features> featuresList;

        public ImageProcessingDialog()
        {
            InitializeComponent();

            //Set up Stopwatch
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_tick);

            opencv = new OpenCvCLR();

            //Mser Properties initialization
            delta = Convert.ToInt32(DeltaNumericUpDown.Value);
            maxVariation = Convert.ToInt32(MVNumericUpDown.Value);
            minDiversity = Convert.ToInt32(MDNumericUpDown.Value);

            featuresList = new List<Features>();
            


            //crops list view init
            //string directoryPath = System.Configuration.ConfigurationManager.AppSettings["cropsDirectory"];
            //List<string> crops=Directory.GetFiles(directoryPath, "*.jpg").ToList();
            //foreach (string cropPath in crops)
            //{
            //    string[] cropPathsplit = cropPath.Split('\\');
            //    uploadCrop(cropPath, cropPathsplit[cropPathsplit.Length - 1]);
            //}


        }

        #region Stopwatch events

        void timer_tick(object sender, EventArgs e)
        {
            TimeSpan ts = stopWatch.Elapsed;
            StopwatchTextBox.Text = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Hours, ts.Minutes, ts.Seconds);
        }

        private void StartStopwatchButton_Click(object sender, EventArgs e)
        {
            if (!timerRunning)
            {
                timer.Start();
                stopWatch.Start();
                timerRunning = true;
            }
        }

        private void stopStopwatchButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            stopWatch.Stop();
            timerRunning = false;
        }

        private void ResetStopwatchLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            stopWatch = Stopwatch.StartNew();
            stopWatch.Stop();
            timer.Stop();
            timerRunning = false;
            StopwatchTextBox.Text = "00:00:00";
        }

        #endregion

        #region ImageListView events

        #region ImageContextMenuStrip events
        private void ImageLargeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageListView.View = View.LargeIcon;
        }

        private void ImageSmallIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageListView.View = View.List;
        }

        private void ImageDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageListView.View = View.Details;
        }

        private void ImageContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            DisableDeleteInContextMenuStrip(ImageListView, ImageContextMenuStrip);
        }

        private void ImageDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteImages(ImageListView);
        }

        #endregion

        private void ImageListView_SelectedIndexChange(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab == TabControl.TabPages["ImagesTabPage"])
            {
                ShowImageAndInfoInForm(ImageListView);
            }

        }

        private void ImageListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenImageZoomCrop(ImageListView);
        }



        private void ImageListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (((Control)sender).Name == "ImageListView")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    OpenImageZoomCrop(ImageListView);
                }

                if (e.KeyCode == Keys.A && e.Control)
                {
                    foreach (ListViewItem item in ImageListView.Items)
                    {
                        item.Selected = true;
                    }
                }

                if (e.KeyCode == Keys.Delete)
                {
                    DeleteImages(ImageListView);
                }
            }
        }


        #endregion

        #region CropListView events

        #region CropContextMenuStrip events

        private void CropLargeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CropListView.View = View.LargeIcon;
        }

        private void CropSmallIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CropListView.View = View.SmallIcon;
        }

        private void CropDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CropListView.View = View.Details;
        }

        private void CropContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            DisableDeleteInContextMenuStrip(CropListView, CropContextMenuStrip);
        }

        private void CropDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteImages(CropListView);
        }

        #endregion

        private void CropListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab == TabControl.TabPages["CropsTabPage"])
            {
                ShowImageAndInfoInForm(CropListView);
            }

        }

        private void CropListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenImageZoomCrop(CropListView);
        }

        private void CropListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (((Control)sender).Name == "CropListView")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    OpenImageZoomCrop(CropListView);
                }

                if (e.KeyCode == Keys.A && e.Control)
                {
                    foreach (ListViewItem item in CropListView.Items)
                    {
                        item.Selected = true;
                    }
                }

                if (e.KeyCode == Keys.Delete)
                {
                    DeleteImages(CropListView);
                }
            }
        }

        #endregion

        #region TrueTargetsListView events

        private void TrueTargetsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenImageZoomCrop(TrueTargetsListView);
        }

        private void TrueTargetsListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (((Control)sender).Name == "TrueTargetsListView")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    OpenImageZoomCrop(TrueTargetsListView);
                }

                if (e.KeyCode == Keys.A && e.Control)
                {
                    foreach (ListViewItem item in TrueTargetsListView.Items)
                    {
                        item.Selected = true;
                    }
                }

                if (e.KeyCode == Keys.Delete)
                {
                    DeleteImages(TrueTargetsListView);
                }
            }
        }

        private void TrueTargetsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab == TabControl.TabPages["TrueTargetsTabPage"])
            {
                ShowImageAndInfoInForm(TrueTargetsListView);
            }
        }

        #endregion

        #region Images events and thread

        private void AddImagesButtom_Click(object sender, EventArgs e)
        {
            if (!ImagesThreadRunning)
            {
                AddImagesButtom.BackColor = Color.PaleGreen;
                ResetImages.Enabled = false;
                ImageThread = new Thread(new ThreadStart(AddImageThread));
                ImageThread.IsBackground = true;
                ImagesThreadRunning = true;
                IsResetClicked = false;
                ImageThread.Start();
                classifyThraed = classificationThread();
            }
            else
            {
                closeImageThread();
                ResetImages.Enabled = true;
            }
        }

        public Thread startCopyThread()
        {
            Thread CopyThread = new Thread(() =>
                DoCopyThread());
            CopyThread.IsBackground = true;
            copyThreadRunning = true;
            CopyThread.Start();
            return CopyThread;
        }

        private void DoCopyThread()
        {
            try
            {
                string targetPath = System.Configuration.ConfigurationManager.AppSettings["AUVSIDirectory"];
                string sourcePath = System.Configuration.ConfigurationManager.AppSettings["AUVSIDirectory"];

                for (int i = 1; i < 100; i++)
                {
                    string directoryNumber = (i >= 10) ? i.ToString() : "0" + i.ToString();
                    if (!System.IO.Directory.Exists(targetPath + directoryNumber))
                    {
                        targetPath = targetPath + directoryNumber;
                        break;
                        //System.IO.Directory.CreateDirectory(targetPath + directoryNumber);
                    }
                }

                if (targetPath == sourcePath)
                {
                    return;
                }

                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                }

                string messageText = String.Format("The files have been copied successfuly");
                MessageBox.Show(messageText, "Coping", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Common.writeToLog(String.Format("ImageProcessingConrol: {0} has  been copied", targetPath));
            }
            catch (Exception ex)
            {
                Common.writeToLog(String.Format("ImageProcessingConrol: CopyDirectory was failed - Exception was thrown {0}", ex.Message));
                MessageBox.Show(string.Format("ImageProcessingConrol: CopyDirectory was failed: Exception was thrown - {0}", ex.Message), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyDirectory_Click(object sender, EventArgs e)
        {
            if (!copyThreadRunning)
            {
                startCopyThread();
            }
            else
            {
                MessageBox.Show("copy is in progress", "Coping", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cleanClassificationDirectory_Click(object sender, EventArgs e)
        {
            
            try
            {
                string inputDirectoryPath = System.Configuration.ConfigurationManager.AppSettings["SVMDirectoryIn"];
                string outputDirectoryPath = System.Configuration.ConfigurationManager.AppSettings["SVMDirectoryOut"];
                string trueTargetsDirectoryPath = System.Configuration.ConfigurationManager.AppSettings["TrueTargetsDirectory"];
                string manualTargetsDirectoryPath = System.Configuration.ConfigurationManager.AppSettings["ManualTargetsDirectory"];
                string doublesTargetsDirectoryPath = System.Configuration.ConfigurationManager.AppSettings["DoublesTargetsDirectory"];
                

                string messageText = String.Format("Are you sure you want to delete these Data from:\n {0},\n{1},\n{2},\n{3}?", inputDirectoryPath, outputDirectoryPath, trueTargetsDirectoryPath,manualTargetsDirectoryPath);
                DialogResult res = MessageBox.Show(messageText, "Deleting", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                if (res == DialogResult.OK)
                {
                    Directory.Delete(inputDirectoryPath, true);
                    Directory.Delete(outputDirectoryPath, true);
                    Directory.Delete(trueTargetsDirectoryPath, true);
                    Directory.Delete(manualTargetsDirectoryPath, true);
                    Directory.CreateDirectory(inputDirectoryPath);
                    Directory.CreateDirectory(outputDirectoryPath);
                    Directory.CreateDirectory(trueTargetsDirectoryPath);
                    Directory.CreateDirectory(manualTargetsDirectoryPath);
                    Directory.CreateDirectory(doublesTargetsDirectoryPath);
                }

                Common.writeToLog(String.Format("ImageProcessingConrol: {0} has been deleted", inputDirectoryPath));
                Common.writeToLog(String.Format("ImageProcessingConrol: {0} has been deleted", outputDirectoryPath));
            }
            catch (Exception ex)
            {
                Common.writeToLog(String.Format("ImageProcessingConrol: cleanClassificationDirectory was failed - Exception was thrown {0}", ex.Message));
                MessageBox.Show(string.Format("ImageProcessingConrol: cleanClassificationDirectoryn was failed: Exception was thrown - {0}", ex.Message), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetImages_Click(object sender, EventArgs e)
        {
            try
            {
                TargetImage removeTarget;
                IsResetClicked = true;
                foreach (ListViewItem item in ImageListView.Items)
                {
                    item.Remove();
                }
                foreach (ListViewItem item in CropListView.Items)
                {
                    item.Remove();
                }

                foreach (ListViewItem item in TrueTargetsListView.Items)
                {
                    item.Remove();
                }
                while (!ToClassifyCrops.IsEmpty)
                {
                    ToClassifyCrops.TryDequeue(out removeTarget);
                }
                sourceImages.Clear();
                crops.Clear();
                


                ImgSetServer source = new ImgSetServer();
                source.reset();
                TargetImagePictureBox.Image = null;
                _ImageNameLabel.Text = String.Empty;
                _ImageLatitudeLabel.Text = String.Empty;
                _ImageLongitudeLabel.Text = String.Empty;
                _TargetLatitudeLabel.Text = String.Empty;
                _TargetLongitudeLabel.Text = String.Empty;
                _LetterColorLabel.Text = String.Empty;
                _ShapeColorLabel.Text = String.Empty;
                _ShapeLabel.Text = String.Empty;
                _YawLabel.Text = String.Empty;
                _OrientationLabel.Text = String.Empty;
                _LetterLabel.Text = String.Empty;


            }
            catch (Exception ex)
            {
                Common.writeToLog(String.Format("ImageProcessingConrol: ResetImage_Click was failed - Exception was thrown {0}", ex.Message));
                MessageBox.Show(string.Format("ImageProcessingConrol: ResetImage_Click was failed: Exception was thrown - {0}", ex.Message), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 

        private void AddImageThread()
        {
            GetImages = true;
            while (GetImages)
            {
                Invoke(new AddImageDelegate(AddImage));
                Thread.Sleep(40);
            }
        }

        private void AddImage()
        {
            string sourceImageName = string.Empty;
            ImgSetServer source = new ImgSetServer();
            string ImagePath = string.Empty;

            try
            {
                
                
                if (source.getNext())
                {
                    string[] tmp = source.src.Split('\\');
                    sourceImageName = tmp[tmp.Length - 1];
                    ImagePath = /*"C:\\temp\\images" +*/ source.src;

                    Image image = Image.FromFile(ImagePath);

                    ImageFromServer sourceImage = new ImageFromServer(source.datetime, source.lat, source.lon, source.altitude ,ImagePath,sourceImageName , source.gRoll, source.gPitch, source.gYaw);
                    sourceImages.TryAdd(sourceImageName, sourceImage);
                    
                    
                    //crops-plane
                    int j = CropListView.Items.Count;
                    foreach (var crop in source.crops)
                    {
                        tmp = crop.src.Split('\\');
                        string cropPath = /*"C:\\temp\\images" +*/ crop.src;
                        string cropName = tmp[tmp.Length - 1];
                        //Add images to LargeImagelist
                        //if (!File.Exists(crop.src))
                        //{
                        //    MessageBox.Show(String.Format("The image {0} does not exist in {1}", tmp[tmp.Length - 1], crop.src), "Image does Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    bigImage.crops.Remove(crop);
                        //    continue;
                        //}
                        //image = Image.FromFile(crop.src);

                        if (!File.Exists(cropPath))
                        {
                            MessageBox.Show(String.Format("The image {0} does not exist in {1}", cropName, crop.src), "Image does Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            source.crops.Remove(crop);
                            continue;
                        }

                        try
                        {
                            Image ImageCrop = Image.FromFile(cropPath);

                            Image largeImageCrop = ImageCrop.GetThumbnailImage(100, 100, null, new IntPtr());
                            Image smallImageCrop = ImageCrop.GetThumbnailImage(16, 16, null, new IntPtr());
                            LargeImageList2.Images.Add(largeImageCrop);
                            SmallImageList2.Images.Add(smallImageCrop);


                            TargetImage targetImage = new TargetImage(source.datetime, source.lat, source.lon, source.altitude, cropPath, cropName, source.gRoll, source.gPitch, source.gYaw, crop.cropcords);
                            crops.TryAdd(cropName, targetImage);
                            sourceImage.cropsInImage.Add(targetImage);
                            ToClassifyCrops.Enqueue(targetImage);

                            ListViewItem item = new ListViewItem();
                            item.ImageIndex = j;
                            CropListView.Items.Add(cropName, j);
                            CropListView.Items[j].Tag = targetImage;
                            CropListView.Items[j].SubItems.Add(targetImage.dateTime.ToString());
                            //CropListView.Items[j].SubItems.Add(targetImage.imageLatitude.ToString());
                            //CropListView.Items[j].SubItems.Add(targetImage.imageLongitude.ToString());
                            CropListView.Items[j].SubItems.Add("");
                            CropListView.Items[j].SubItems.Add("");
                            CropListView.Items[j].SubItems.Add(targetImage.sourceFile);
                            //CropListView.Items[j].SubItems.Add(targetImage.isRealTarget.ToString());
                            targetImage.indexListView = j;

                            if (CropListView.SelectedIndices.Count == 1)
                            {
                                if (j == CropListView.SelectedIndices[0] + 1)
                                {
                                    CropListView.Items[j - 1].Selected = false;
                                    CropListView.Items[j].Selected = true;
                                    CropListView.Items[j].EnsureVisible();
                                }
                            }

                            if (j == 0)
                            {
                                CropListView.Items[j].Selected = true;
                            }

                            j++;

                            double[] Xvalues = new double[] { crop.cropcords[0].X, crop.cropcords[1].X, crop.cropcords[2].X, crop.cropcords[3].X };
                            double[] Yvalues = new double[] { crop.cropcords[0].Y, crop.cropcords[1].Y, crop.cropcords[2].Y, crop.cropcords[3].Y };

                            float homeLat = float.Parse(System.Configuration.ConfigurationManager.AppSettings["homeLatitude"]);
                            float homeLong = float.Parse(System.Configuration.ConfigurationManager.AppSettings["homeLongitude"]);
                            float homeAlt = float.Parse(System.Configuration.ConfigurationManager.AppSettings["homeAltitude"]);

                            GPSThreadInfo info = new GPSThreadInfo(cropName, (float)targetImage.gYaw, 0.0f /*(float)targetImage.gPitch*/, 0.0f /*(float)targetImage.gRoll*/, HomeLat, HomeLon, HomeAlt, (float)targetImage.imageLatitude, (float)targetImage.imageLongitude, (float)targetImage.imageAltitude, (int)Xvalues.Average(), (int)Yvalues.Average());
                            ThreadPool.QueueUserWorkItem(new WaitCallback(DoGPSCoordsThread), info);

                        }
                        catch (OutOfMemoryException ex)
                        {
                            DialogResult res = MessageBox.Show(String.Format("The file {0} does not have a valid image format.\n {1}", cropName, ex.Message), "Image does Not Exist", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            Common.writeToLog(String.Format("The file {0} does not have a valid image format.\n {1}", cropName, ex.Message));
                            source.crops.Remove(crop);
                            if (res == DialogResult.Cancel)
                            {
                                closeImageThread();
                                ResetImages.Enabled = true;
                            }
                            continue;
                        }
                        catch (FileNotFoundException ex)
                        {
                            DialogResult res = MessageBox.Show(String.Format("The file {0} does not exist in {1}", cropName, crop.src), "Image does Not Exist", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            Common.writeToLog(String.Format("The file {0} does not exist in {1}", cropName, crop.src));
                            source.crops.Remove(crop);
                            if (res == DialogResult.Cancel)
                            {
                                closeImageThread();
                                ResetImages.Enabled = true;
                            }
                            continue;
                        }
                        catch (Exception ex)
                        {
                            DialogResult res = MessageBox.Show(String.Format("ImageProcessingDialog: addImage threw exception: {0}", ex.Message), "Add Images failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            Common.writeToLog(String.Format("ImageProcessingDialog: addImage threw exception: {0}", ex.Message));
                            source.crops.Remove(crop);
                            if (res == DialogResult.Cancel)
                            {
                                closeImageThread();
                                ResetImages.Enabled = true;
                            }
                            continue;
                        }
                    }
                    
                    
                    int i = ImageListView.Items.Count;

                    //Adding sourceImage to ImageListView

                    //TODO improve quality 
                    Image largeImage = image.GetThumbnailImage(100, 100, null, new IntPtr());
                    Image smallImage = image.GetThumbnailImage(16, 16, null, new IntPtr());

                    LargeImageList1.Images.Add(largeImage);
                    SmallImageList1.Images.Add(smallImage);

                    ImageListView.Items.Add(sourceImageName, i);
                    ImageListView.Items[i].Tag = sourceImage;
                    ImageListView.Items[i].SubItems.Add(sourceImage.dateTime.ToString());
                    ImageListView.Items[i].SubItems.Add(sourceImage.imageLatitude.ToString());
                    ImageListView.Items[i].SubItems.Add(sourceImage.imageLongitude.ToString());
                    ImageListView.Items[i].SubItems.Add(source.src);
                    if (ImageListView.SelectedIndices.Count == 1)
                    {
                        if (i == ImageListView.SelectedIndices[0] + 1)
                        {
                            ImageListView.Items[i - 1].Selected = false;
                            ImageListView.Items[i].Selected = true;
                            ImageListView.Items[i].EnsureVisible();

                        }
                    }

                    if (i == 0)
                    {
                        ImageListView.Items[i].Selected = true;
                    }
                    i++;
                    
                }
            }
            catch (OutOfMemoryException ex)
            {
                DialogResult res = MessageBox.Show(String.Format("The file {0} does not have a valid image format.\n {1})", sourceImageName, ex.Message), "Image Not Exist", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                Common.writeToLog(String.Format("The file {0} does not have a valid image format.\n {1}", sourceImageName, ex.Message));
                if (res == DialogResult.Cancel)
                {
                    closeImageThread();
                    ResetImages.Enabled = true;
                }
            }
            catch (FileNotFoundException ex)
            {
                DialogResult res = MessageBox.Show(String.Format("The file {0} not exist in {1}", sourceImageName, ImagePath), "Image Not Exist", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                Common.writeToLog(String.Format("The file {0} not exist in {1}", sourceImageName, ImagePath));
                if (res == DialogResult.Cancel)
                {
                    closeImageThread();
                    ResetImages.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Common.writeToLog(String.Format("ImageProcessingConrol: AddImage was failed - Exception was thrown {0}", ex.Message));
                DialogResult res = MessageBox.Show(string.Format("ImageProcessingConrol: AddImage was failed: Exception was thrown - {0}", ex.Message), "Error Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (res == DialogResult.Cancel)
                {
                    closeImageThread();
                    ResetImages.Enabled = true;
                }

            }

        }

        #endregion

        #region Mser thread

        //public Thread startMserThread(String filePath)
        //{
        //    Thread MSERThread = new Thread(() =>
        //        DoMSERThread(filePath));
        //    MSERThread.IsBackground = true;
        //    mserThreadRunning = true;
        //    MSERThread.Start();
        //    return MSERThread;
        //}

        private void DoMSERThread(object info)
        {
            string filePath = info as string;
            opencv.runMSER_CLR(filePath, delta, maxVariation, minDiversity);
        }

        #endregion

        #region GPS thread
        //public Thread startGPSCoordsThread(string cropName, float sourceYaw, float sourcePitch, float sourceRoll, float LatHome, float LongHome, float AltHome, float LatSource, float LongSource, float AltSouce, int xPixel, int yPixel)
        //{
        //    Thread GPSThread = new Thread(() =>
        //        DoGPSCoordsThread(cropName,sourceYaw, sourcePitch, sourceRoll, LatHome, LongHome, AltHome, LatSource,   LongSource,   AltSouce,   xPixel,   yPixel));
        //    GPSThread.IsBackground = true;
        //    //GPSCoordsThreads.Add(GPSThread);
        //    GPSThread.Start();
        //    return GPSThread;
        //}


        private void DoGPSCoordsThread(object info)
         {
             GPSThreadInfo inputInfo = info as GPSThreadInfo;
             string[] result = opencv.runGPSCoords_CLR(inputInfo.sourceYaw, inputInfo.sourcePitch, inputInfo.sourceRoll, inputInfo.LatHome, inputInfo.LongHome, inputInfo.AltHome, inputInfo.LatSource, inputInfo.LongSource, inputInfo.AltSouce, inputInfo.xPixel, inputInfo.yPixel);
            int index;

             GPSMut.WaitOne();
             crops[inputInfo.cropName].targetLatitudeDeg = Convert.ToDouble(result[0]);
             crops[inputInfo.cropName].targetLongitudeDeg = Convert.ToDouble(result[1]);
             crops[inputInfo.cropName].targetLatitudeMinute = result[2];
             crops[inputInfo.cropName].targetLongitudeMinute = result[3];
             index = crops[inputInfo.cropName].indexListView;
//             PointLatLng coords = new PointLatLng(crops[inputInfo.cropName].targetLatitudeDeg, crops[inputInfo.cropName].targetLongitudeDeg);
             GPSMut.ReleaseMutex();

             SetLonAndLatInListCropView(index, result[0], result[1]);
            // markers.Add(coords);
            //if (googleIsRunning == true)
            //{
            //    GoogleMapdlg.addMarker(coords);
            //}
         }

        #endregion
 


        private void GMapButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (googleIsRunning == false)
                {
                    GoogleMapdlg = new GoogleMapDialog(markers);
                    Thread GMapThread = new Thread(() =>
                    {
                        Application.Run(GoogleMapdlg);
                    });
                    googleIsRunning = true;
                    GMapThread.SetApartmentState(ApartmentState.STA);
                    GMapThread.Start();
                }
            }
            catch (Exception ex)
            {
                Common.writeToLog(String.Format("ImageProcessingConrol: GMapButton_Click was failed - Exception was thrown {0}", ex.Message));
                MessageBox.Show(string.Format("ImageProcessingConrol: GMapButton_Click was failed: Exception was thrown - {0}", ex.Message), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #region private method

        private void ShowImageAndInfoInForm(ListView listView)
        {
            if (listView.SelectedItems.Count == 0)
            {
                TargetImagePictureBox.Image = null;
                _ImageNameLabel.Text = String.Empty;
                _ImageLatitudeLabel.Text = String.Empty;
                _ImageLongitudeLabel.Text = String.Empty;
                _TargetLatitudeLabel.Text = String.Empty;
                _TargetLongitudeLabel.Text = String.Empty;
                _LetterColorLabel.Text = String.Empty;
                _ShapeColorLabel.Text = String.Empty;
                _ShapeLabel.Text = String.Empty;
                _YawLabel.Text = String.Empty;
                _OrientationLabel.Text = String.Empty;
                _LetterLabel.Text = String.Empty;
            }
            foreach (ListViewItem item in listView.SelectedItems)
            {
                int imgIndex = item.ImageIndex;
                if (imgIndex >= 0 && imgIndex < listView.Items.Count)
                {
                    if (listView.Name == "CropListView" || listView.Name == "TrueTargetsListView")
                    {
                        var img = (TargetImage)item.Tag;
                        //TargetImagePictureBox.Image = Image.FromFile(img.sourceFile);
                        TargetImagePictureBox.Image = Image.FromFile(img.sourceFile);
                        //TargetImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;


                        _ImageNameLabel.Text = img.imageName.ToString();
                        _ImageLatitudeLabel.Text = img.imageLatitude.ToString();
                        _ImageLongitudeLabel.Text = img.imageLongitude.ToString();

                        GPSMut.WaitOne();
                        _TargetLatitudeLabel.Text = img.targetLatitudeMinute;
                        _TargetLongitudeLabel.Text = img.targetLongitudeMinute;
                        GPSMut.ReleaseMutex();

                        classifyMut.WaitOne();
                        _ShapeLabel.Text = img.shape;
                        _ShapeColorLabel.Text = img.targetColor;
                        _LetterLabel.Text = img.letter;
                        _LetterColorLabel.Text = img.letterColor;
                        _OrientationLabel.Text = img.orientation;
                        classifyMut.ReleaseMutex();

                        _YawLabel.Text = img.gYaw.ToString();

                    }
                    else
                    {
                        var img = (ImageFromServer)item.Tag;
                        //TargetImagePictureBox.Image = Image.FromFile(img.sourceFile);
                        TargetImagePictureBox.Image = Image.FromFile(img.sourceFile);
                       //TargetImagePictureBox.SizeMode = PictureBoxSizeMode.Zoom;


                        _ImageNameLabel.Text = img.imageName.ToString();
                        _ImageLatitudeLabel.Text = img.imageLatitude.ToString();
                        _ImageLongitudeLabel.Text = img.imageLongitude.ToString();
                        _TargetLatitudeLabel.Text = String.Empty;
                        _TargetLongitudeLabel.Text = String.Empty;
                        _ShapeLabel.Text = String.Empty;
                        _ShapeColorLabel.Text = String.Empty;
                        _LetterLabel.Text = String.Empty;
                        _LetterColorLabel.Text = String.Empty;
                        _YawLabel.Text = img.gYaw.ToString();
                    }
                }
            }
        }

        private void closeImageThread()
        {
            AddImagesButtom.BackColor = Color.Transparent;
            GetImages = false;
            ImagesThreadRunning = false;
            ImageThread.Abort();
            

        }

        private void DeleteImages(ListView listView)
        {

            int numOfSelectedImages = listView.SelectedItems.Count;

            //if (numOfSelectedImages == 0)
            //{
            //    MessageBox.Show("", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            string messageText = String.Format("Are you sure you want to delete these {0} images from {1}?", numOfSelectedImages, listView.Name);
            DialogResult res = MessageBox.Show(messageText, "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (res == DialogResult.Yes)
            {
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    bool fleg = false;
                    listView.Items.Remove(item);
                    string imgName = ((ImageFromServer)item.Tag).imageName;
                    if (listView.Name == "ImageListView")
                    {
                        ImageFromServer removeImage;
                        
                        while (!fleg)
                        {

                            fleg = sourceImages.TryRemove(imgName, out removeImage);
                        }
                    }
                    else
                    {
                        TargetImage removeTraget;

                        while (!fleg)
                        {
                            fleg = crops.TryRemove(imgName, out removeTraget);
                        }
                    }
                }
                TargetImagePictureBox.Image = null;
                _ImageNameLabel.Text = String.Empty;
                _ImageLatitudeLabel.Text = String.Empty;
                _ImageLongitudeLabel.Text = String.Empty;
                _TargetLatitudeLabel.Text = String.Empty;
                _TargetLongitudeLabel.Text = String.Empty;
                _LetterColorLabel.Text = String.Empty;
                _ShapeColorLabel.Text = String.Empty;
                _ShapeLabel.Text = String.Empty;
                _YawLabel.Text = String.Empty;
                _OrientationLabel.Text = String.Empty;
                _LetterLabel.Text = String.Empty;

                Common.writeToLog(String.Format("{0} images was deleted from {1}.", numOfSelectedImages,listView.Name));//check

            }
        }

        private void DisableDeleteInContextMenuStrip(ListView listView, ContextMenuStrip contextMenuStrip)
        {
            if (listView.SelectedItems.Count == 0)
            {
                contextMenuStrip.Items[1].Enabled = false;
            }
            else
            {
                contextMenuStrip.Items[1].Enabled = true;
            }
        }

        private void OpenImageZoomCrop(ListView listView)
        {
            if (listView.SelectedItems.Count != 1)
            {
                return;
                //throw something
            }

            ListViewItem item = listView.SelectedItems[0];
            string fileName = String.Empty;

            if (listView.Name == "ImageListView")
            {
                //string fileName = ((ImageFromServer)item.Tag).sourceFile;
                String imageName = ((ImageFromServer)item.Tag).sourceFile;
                 //fileName = "C:/temp/images" + imageName;
                 fileName = imageName;
            }
            else
            {
                //string fileName = ((TargetImage)item.Tag).sourceFile;
                String imageName = ((TargetImage)item.Tag).sourceFile;
                 //fileName = "C:/temp/images" + imageName;
                fileName = imageName;
            }



            //var newImage = Image.FromFile(((TargetImage)item.Tag).sourceFile);
            var newImage = Image.FromFile(fileName);



            var newForm = new ImageZoomCrop(fileName, true, ((ImageFromServer)item.Tag), true);

            newForm.pbImage.Image = newImage;
            newForm.Show();
        }
        #endregion

        #region classificationThread

        private Thread classificationThread()
        {
            Thread classificationThread = new Thread(() =>
                DoclassificationThread());
            classificationThread.IsBackground = true;
            classificationThreadRunning = true;
            classificationThread.Start();
            return classificationThread;
        }

        private bool distanceIsLessThan(double lat1, double lon1, double lat2, double lon2, int threshold)
        {
            //calculate the general distance - remains for further use if needed.
            double fd2r = Math.PI / 180;
            double a = Math.Pow(Math.Sin((lat2-lat1)*fd2r/2.0), 2) + Math.Cos( lat1 * fd2r) * Math.Cos(lat2 * fd2r) * Math.Pow(Math.Sin((lon2-lon1)*fd2r/2.0), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1-a));
            double d = 6371.0 * c;
            if ((float)d * 1000 < threshold)
                return true;
            
            return false;
        }

        private bool similarFeatures(Features f1, Features f2, int threshold, int numberOfFeatures)
        {
            int num = 1;
            bool areClose = distanceIsLessThan(f1.location.Lat, f1.location.Lng, f2.location.Lat, f2.location.Lng, threshold);
            if (areClose)
                num++;
            if (f1.letter == f2.letter)
                num++;
  //          if (f1.shape == f2.shape)
  //              num++;
            if (num >= numberOfFeatures)
                return true;
            return false;
        }


        private string changeColorName(string color)
        {
            //string fixedColor = color.Substring(1, color.Length - 2);
            string fixedColor = color.Split(':')[0];
            switch (fixedColor)
            {
                case "none":
                    return "None";
                case "white":
                case "snow":
                case "honeydew":
                case "mintcream":
                case "azure":
                case "aliceblue":
                case "ghostwhite":
                case "whitesmoke":
                case "seashell":
                case "beige":
                case "oldlace":
                case "floralwhite":
                case "ivory":
                case "antiquewhite":
                case "linen":
                case "lavenderblush":
                case "mistyrose":
                    return "White";
                case "black":
                    return "Black";
                case "red":
                case "lightsalmon":
                case "salmon":
                case "darksalmon":
                case "lightcoral":
                case "indianred":
                case "crimson":
                case "firebrick":
                case "darkred":
                    return "Red";
                case "pink":
                case "lightpink":
                case "hotpink":
                case "deeppink":
                case "palevioletred":
                case "mediumvioletred":
                    return "Pink";
                case "orange":
                case "darkorange":
                case "coral":
                case "tomato":
                case "orangered":
                    return "Orange";
                case "yellow":
                case "lightyellow":
                case "lemonchiffon":
                case "lightgoldenrodyellow":
                case "papayawhip":
                case "moccasin":
                case "peachpuff":
                case "palegoldenrod":
                case "khaki":
                case "darkkhaki":
                case "gold":
                    return "Yellow";
                case "brown":
                case "cornsilk":
                case "blanchedalmond":
                case "bisque":
                case "navajowhite":
                case "wheat":
                case "burlywood":
                case "tan":
                case "rosybrown":
                case "sandybrown":
                case "goldenrod":
                case "darkgoldenrod":
                case "peru":
                case "chocolate":
                case "saddlebrown":
                case "sienna":
                case "maroon":
                    return "Brown";
                case "green":
                case "palegreen":
                case "lightgreen":
                case "yellowgreen":
                case "greenyellow":
                case "chartreuse":
                case "lawngreen":
                case "lime":
                case "limegreen":
                case "mediumspringgreen":
                case "springgreen":
                case "mediumaquamarine":
                case "aquamarine":
                case "lightseagreen":
                case "mediumseagreen":
                case "seagreen":
                case "darkseagreen":
                case "forestgreen":
                case "darkgreen":
                case "olivedrab":
                case "olive":
                case "darkolivegreen":
                case "teal":
                    return "Green";
                case "blue":
                case "lightblue":
                case "powderblue":
                case "paleturquoise":
                case "turquoise":
                case "mediumturquoise":
                case "darkturquoise":
                case "lightcyan":
                case "cyan":
                case "aqua":
                case "darkcyan":
                case "cadetblue":
                case "lightsteelblue":
                case "steelblue":
                case "lightskyblue":
                case "skyblue":
                case "deepskyblue":
                case "dodgerblue":
                case "cornflowerblue":
                case "royalblue":
                case "mediumblue":
                case "darkblue":
                case "navy":
                case "midnightblue":
                    return "Blue";
                case "purple":
                case "lavender":
                case "thistle":
                case "plum":
                case "violet":
                case "orchid":
                case "fuchsia":
                case "magenta":
                case "mediumorchid":
                case "mediumpurple":
                case "amethyst":
                case "blueviolet":
                case "darkviolet":
                case "darkorchid":
                case "darkmagenta":
                case "slateblue":
                case "darkslateblue":
                case "mediumslateblue":
                case "indigo":
                    return "Purple";
                case "gray":
                case "gainsboro":
                case "lightgray":
                case "silver":
                case "darkgray":
                case "dimgray":
                case "lightslategray":
                case "slategray":
                case "darkslategray":
                    return "Grey";
                default:
                    return "None";
            }
        }


        //private string fixOrientation(string originalOrientation, double imageYaw)
        //{
        //    double angle;
        //    switch (originalOrientation)
        //    {
        //        case "N": angle = 0; break;
        //        case "NE": angle = 45; break;
        //        case "E": angle = 90; break;
        //        case "SE": angle = 135; break;
        //        case "S": angle = 180; break;
        //        case "SW": angle = -135; break;
        //        case "W": angle = -90; break;
        //        case "NW": angle = -45 ; break;
        //    }
        //    return originalOrientation;
        //}

        private void DoclassificationThread()
        {
            string fileName = String.Empty;
            //try
            //{

                spatialSvm obj = new spatialSvm();
                //int num_out = 3;
                int num_out = 6;
                int ReadInitFiles_num_out = 2;

                MWArray[] SVMinit = obj.ReadInitFiles(ReadInitFiles_num_out);

                while ((!ToClassifyCrops.IsEmpty || ImagesThreadRunning) && !IsResetClicked)
                {
                    //checks if the queue is empty.
                    if (ToClassifyCrops.IsEmpty)
                    {
                        continue;
                    }
                    TargetImage currentCrop;
                    //try to dequeue the first element in the queue.
                    if (!ToClassifyCrops.TryDequeue(out currentCrop))
                    {
                        continue;
                    }
                    string filePath = currentCrop.sourceFile;
                    fileName = currentCrop.imageName;
                    double yaw = currentCrop.gYaw;
                    //MWArray[] SVMresult = obj.AnalyzeCrop(num_out, filePath, SVMinit[0], SVMinit[1]);
                    MWArray[] SVMresult = obj.AnalyzeCrop(num_out, filePath, SVMinit[0], SVMinit[1], yaw);
                    double[,] SVMvalues = (double[,])SVMresult[0].ToArray();

                    //bool TFresult = (MWLogicalArray)obj.TestSpatialFeatures(filePath);
                    TargetImage target = crops[fileName];

                    target.isChecked = true;

                    classifyMut.WaitOne();
                    target.isRealTarget = Convert.ToInt32(SVMvalues[0, 0]);
//                    target.letterColor = changeColorName((string)SVMresult[1].ToString());
//                    target.targetColor = changeColorName((string)SVMresult[2].ToString());
                    target.letterColor = SVMresult[1].ToString().Split(':')[0];
                    target.targetColor = SVMresult[2].ToString().Split(':')[0];
                    target.letter = SVMresult[3].ToString();
                    target.orientation = SVMresult[4].ToString();
                    target.shape = SVMresult[5].ToString();
                    classifyMut.ReleaseMutex();
                    int indexListView = currentCrop.indexListView;
                    if (target.imageLatitude == 0 || target.imageLongitude == 0)
                    {
                        SetIsRealTaregtInCropListView(indexListView, "Manual");
                        target.isRealTarget = 2;
                    }
                    if (target.isRealTarget == 1)
                    {
                        SetIsRealTaregtInCropListView(indexListView, "true");
                    }
                    else if (target.isRealTarget == 0)
                    {
                        SetIsRealTaregtInCropListView(indexListView, "false");
                    }
                    else
                    {
                        SetIsRealTaregtInCropListView(indexListView, "Manual");
                    }


                    if (currentCrop.isRealTarget == 1)
                    {
						trueTargetCounter++;

                        
                        PointLatLng coords = new PointLatLng(target.targetLatitudeDeg, target.targetLongitudeDeg);
                        

                        /*multiple identification check*/
                        int threshold = 20; //in meters
                        bool continueToNextTarget = false;
                        //if (coords.Lat == 0 || coords.Lng == 0)
                        //    continueToNextTarget = true;
                        if (continueToNextTarget == false)
                        {
                            if (featuresList.Count != 0)
                            {

                                foreach (Features f in featuresList)
                                {
                                    //if (distanceIsLessThan(coords.Lat, coords.Lng, f.location.Lat, f.location.Lng, threshold))
                                    if (similarFeatures(f, new Features(coords, target.letter, target.shape), threshold, 2))
                                    {
                                        continueToNextTarget = true;
                                    }
                                }
                            }
                        }
                    
                        if (continueToNextTarget)
                        {
                            //copy crop to manual folder, and a file that includes all the crop charachteristics
                            System.IO.File.Copy(target.sourceFile, @"C:\AUVSI\true_targets\doubles\" + fileName, true);
                            //create text file for the specific target
                            string manualCropPath = @"C:\AUVSI\true_targets\doubles\" + fileName + ".txt";
                            if (!File.Exists(manualCropPath))
                            {
                                StreamWriter sw = File.CreateText(manualCropPath);
                                sw.Dispose();
                            }
                            //empty the text file
                            File.WriteAllText(manualCropPath, String.Empty);
                            // Create a file to write to. 
                            using (StreamWriter sw = File.CreateText(manualCropPath))
                            {
                                sw.WriteLine("LatitudeMinute: " + target.targetLatitudeMinute);
                                sw.WriteLine("LongitudeMinute " + target.targetLongitudeMinute);
                                sw.WriteLine("LatitudeDegrees: " + target.targetLatitudeDeg);
                                sw.WriteLine("LongitudeDegrees: " + target.targetLongitudeDeg);
                                sw.WriteLine("Orientation: " + target.orientation);
                                sw.WriteLine("Shape: " + target.shape);
                                sw.WriteLine("Background Color: " + target.targetColor);
                                sw.WriteLine("Alphanumeric: " + target.letter);
                                sw.WriteLine("Alphanumeric Color: " + target.letterColor);
                                string[] temp = target.sourceFile.Split('\\', '.');
                                sw.WriteLine("Crop File Name: " + temp[temp.Length - 2]);
                            }
                            trueTargetCounter--;
                            continue;
                        }

                        featuresList.Add(new Features(coords, target.letter, target.shape));

                        Image TrueTarget = Image.FromFile(target.sourceFile);
                        Image LargeTrueTarget = TrueTarget.GetThumbnailImage(100, 100, null, new IntPtr());
                        Image SmallTrueTarget = TrueTarget.GetThumbnailImage(16, 16, null, new IntPtr());

                        AddTrueTarget(target, SmallTrueTarget, LargeTrueTarget);

                        markers.Add(coords);
                        if (googleIsRunning == true)
                        {
                            GoogleMapdlg.addMarker(coords);
                        }

                        //string[] temp = target.sourceFile.Split('\\', '.');
                        //string cropName = "target" + 

                        string targetNum = trueTargetCounter.ToString("00");

                        //copy the crop to the true_targets folder, with a new name: target##.jpg
                        System.IO.File.Copy(target.sourceFile, @"C:\AUVSI\true_targets\target" + targetNum + ".jpg", true);

                        //create text file for the specific target
                        string cropPath = @"C:\AUVSI\true_targets\target" + targetNum + ".txt";
                        if (!File.Exists(cropPath))
                        {
                            StreamWriter sw = File.CreateText(cropPath);
                            sw.Dispose();
                        }

                        //empty the text file
                        File.WriteAllText(cropPath, String.Empty);

                        // Create a file to write to. 
                        using (StreamWriter sw = File.CreateText(cropPath))
                        {
                            sw.WriteLine("LatitudeMinute: " + target.targetLatitudeMinute);
                            sw.WriteLine("LongitudeMinute " + target.targetLongitudeMinute);
                            sw.WriteLine("LatitudeDegrees: " + target.targetLatitudeDeg);
                            sw.WriteLine("LongitudeDegrees: " + target.targetLongitudeDeg);
                            sw.WriteLine("Orientation: " + target.orientation);
                            sw.WriteLine("Shape: " + target.shape);
                            sw.WriteLine("Background Color: " + target.targetColor);
                            sw.WriteLine("Alphanumeric: " + target.letter);
                            sw.WriteLine("Alphanumeric Color: " + target.letterColor);
                            string[] temp = target.sourceFile.Split('\\', '.');
                            sw.WriteLine("Crop File Name: " + temp[temp.Length-2]);
                                
                        }
                        

                        //add the target specifications to the general output text file - for the judges :)
                        string targetDataPath = @"C:\AUVSI\TAS.txt";
                        if (!File.Exists(cropPath))
                        {
                            StreamWriter sw = File.CreateText(targetDataPath);
                            sw.Dispose();
                        }

                        //if this is the first target, empty the file
                        if (trueTargetCounter == 1)
                        {
                            File.WriteAllText(targetDataPath, String.Empty);
                        }

                        // Create a file to write to. 
                        using (StreamWriter sw = File.AppendText(targetDataPath))
                        {
                            sw.Write(targetNum + "\t");
                            sw.Write(target.targetLatitudeMinute + "\t");
                            sw.Write(target.targetLongitudeMinute + "\t");
                            sw.Write(target.orientation + "\t");
                            sw.Write(target.shape + "\t");
                            sw.Write(target.targetColor + "\t");
                            sw.Write(target.letter + "\t");
                            sw.Write(target.letterColor + "\t");
                            sw.WriteLine("target" + targetNum + ".jpg");
                        }
                        
                    }

                    //manual target... :)
                    if (currentCrop.isRealTarget == 2)
                    {
                        // copy the manual target crop, to manual_targets folder. without a text file
                        System.IO.File.Copy(target.sourceFile, @"C:\AUVSI\manual_targets\" + fileName, true);
                    } 

                }
            //}         
            //catch (Exception ex)
            //{
            //    Common.writeToLog(String.Format("ImageProcessingConrol: Doclassification was failed - Exception was thrown {0}", ex.Message));
            //    MessageBox.Show(string.Format("ImageProcessingConrol: Doclassification was failed: Exception was thrown - {0}", ex.Message), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}


            //var newImage = Image.FromFile(((TargetImage)item.Tag).sourceFile);
            string resultPath = System.Configuration.ConfigurationManager.AppSettings["classificationResult"] + fileName;

            //Thread ShowDebugThread = new Thread(() =>
            //{
            //    try
            //    {
            //        var newImage = Image.FromFile(resultPath);
            //        var newForm = new ImageZoomCrop(resultPath, false, null, false);
            //        newForm.pbImage.Image = newImage;
            //        Application.Run(newForm);
            //    }
            //    catch (System.IO.FileNotFoundException e)
            //    {
            //        MessageBox.Show(resultPath);
            //        Environment.Exit(0);
            //    }

            //});
            //ShowDebugThread.SetApartmentState(ApartmentState.STA);
            //ShowDebugThread.Start();

        }


        #endregion


        #region ping thread

        private void PingButton_Click(object sender, EventArgs e)
        {
            if (!pingThreadRunning)
            {
                PingButton.BackColor = Color.PaleGreen;
                Thread PingThread = new Thread(new ThreadStart(DoPingThread));
                PingThread.IsBackground = true;
                pingThreadRunning = true;
                PingTextBox.ReadOnly = true;
                PingThread.Start();
            }
            else
            {
                PingButton.BackColor = Color.Transparent;
                PingTextBox.ReadOnly = false;
                GetPing = false;
                pingThreadRunning = false;
                PingLabel.Text = String.Empty;

            }
        



        }

        private void DoPingThread()
        {
            GetPing = true;
            while (GetPing)
            {
                Invoke(new DoPingDelegate(DoPing));
                Thread.Sleep(30000);
            }
        }

        private void DoPing()
        {

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            try
            {
                PingReply reply = pingSender.Send(PingTextBox.Text, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    PingLabel.Text = "Connected";
                    PingLabel.ForeColor = Color.Blue;

                }
                else
                {
                    PingLabel.Text = "Not Connected";
                    PingLabel.ForeColor = Color.Red;

                }
            }
            catch (Exception ex)
            {
                PingButton.BackColor = Color.Transparent;
                PingTextBox.ReadOnly = false;
                GetPing = false;
                pingThreadRunning = false;
                PingLabel.Text = String.Empty;
                Common.writeToLog(String.Format("ImageProcessingConrol: SpeedTestButton_Click was failed - Exception was thrown {0}", ex.Message));
                MessageBox.Show(string.Format("ImageProcessingConrol: SpeedTestButton_Click was failed: Exception was thrown - {0}", ex.Message), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //MOSTLY HOST NOT FOUND
            }
        }

        #endregion

        #region Mser Properties

        private void SetButton_Click(object sender, EventArgs e)
        {
            delta = Convert.ToInt32(DeltaNumericUpDown.Value);
            maxVariation = Convert.ToInt32(MVNumericUpDown.Value);
            minDiversity = Convert.ToInt32(MDNumericUpDown.Value);
            minArea = Convert.ToInt32(MaxANumericUpDown.Value);
            maxArea = Convert.ToInt32(MinANumericUpDown.Value);

            Common.sendQueryAsync("setmser", "_delta", delta.ToString());
            Common.sendQueryAsync("setmser", "_max_variation", maxVariation.ToString());
            Common.sendQueryAsync("setmser", "_min_diversity", minDiversity.ToString());
            Common.sendQueryAsync("setmser", "_min_area", minArea.ToString());
            Common.sendQueryAsync("setmser", "_max_area", maxArea.ToString());

        }

        #endregion


        private void uploadCrop(string cropPath,string cropName)
        {
            int index = CropListView.Items.Count;
            Image image = Image.FromFile(cropPath);
            Image largeImage = image.GetThumbnailImage(100, 100, null, new IntPtr());
            Image smallImage = image.GetThumbnailImage(16, 16, null, new IntPtr());
            LargeImageList2.Images.Add(largeImage);
            SmallImageList2.Images.Add(smallImage);
            CropListView.Items.Add(cropName, index);
            //TODO -----------!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        private void TargetImagePictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (TabControl.SelectedTab == TabControl.TabPages["ImagesTabPage"])
            {
                 System.Reflection.PropertyInfo pInfo = this.TargetImagePictureBox.GetType().GetProperty("ImageRectangle", System.Reflection.BindingFlags.Public
                    | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                Rectangle imageRect = (Rectangle)pInfo.GetValue(this.TargetImagePictureBox, null);
                double ratioX = imageRect.Width / double.Parse(System.Configuration.ConfigurationManager.AppSettings["ScaledDownWidth"]);
                double ratioY = imageRect.Height / double.Parse(System.Configuration.ConfigurationManager.AppSettings["ScaledDownHeight"]);
                foreach (ListViewItem item in ImageListView.SelectedItems)
                {
                    int imgIndex = item.ImageIndex;
                    if (imgIndex >= 0 && imgIndex < ImageListView.Items.Count)
                    {

                        var imgFromServer = (ImageFromServer)item.Tag;
                        Common.drawMserResults(e, imgFromServer, ratioX, ratioY, imageRect.X, imageRect.Y);
                    }
                }
            }
            else
            {

                double yaw;

                if (TabControl.SelectedTab == TabControl.TabPages["CropsTabPage"])
                {
                    if (CropListView.SelectedItems.Count != 1)
                        return;
                    ListViewItem item = CropListView.SelectedItems[0];
                    yaw = ((ImageFromServer)item.Tag).gYaw;
                }

                else
                {
                    if (TrueTargetsListView.SelectedItems.Count != 1)
                        return;
                    ListViewItem item = TrueTargetsListView.SelectedItems[0];
                    yaw = ((ImageFromServer)item.Tag).gYaw;
                }
                //draw compass rose
                int radius = 200;
                int offsetX = TargetImagePictureBox.Location.X;
                int offsetY = TargetImagePictureBox.Location.Y;
                int centerX = TargetImagePictureBox.Size.Width / 2 + offsetX; 
                int centerY = TargetImagePictureBox.Size.Height / 2 + offsetY;
                int rsin = (int)(radius * Math.Sin(yaw * Math.PI / 180));
                int rcos = (int)(radius * Math.Cos(yaw * Math.PI / 180));

                Point center = new Point(centerX, centerY);
                Point north = new Point(centerX - rsin, centerY - rcos);
                Point south = new Point(centerX + rsin, centerY + rcos);
                Point east = new Point(centerX + rcos, centerY - rsin);
                Point west = new Point(centerX - rcos, centerY + rsin);

                drawDirectionLine(e, System.Drawing.Color.DarkOrchid, center, south, 2, false);
                drawDirectionLine(e, System.Drawing.Color.Orange, center, east, 2, false);
                drawDirectionLine(e, System.Drawing.Color.OrangeRed, center, west, 2, false);
                drawDirectionLine(e, System.Drawing.Color.DarkCyan, center, north, 5, true);
            }
        }

        private void drawDirectionLine(PaintEventArgs e, System.Drawing.Color color, Point center, Point end, int width, bool arrow)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(color, width);
            if (arrow)
            {
                pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;
            }
            e.Graphics.DrawLine(pen, center, end);
            pen.Dispose();
        }

        private void RunMserButton_Click(object sender, EventArgs e)
        {
            string[] filePaths = Directory.GetFiles(@"c:\temp\images\", "*.jpg");
            foreach (string filePath in filePaths)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(DoMSERThread), filePath);
            }
        }

        #region camera control
        private void StartShootButton_Click(object sender, EventArgs e)
        {
            string res = string.Empty;
            if (string.IsNullOrEmpty(zoomTextBox.Text))
            {
                res = Common.sendQuery("startshoot");
            }
            else
            {
                res = Common.sendQuery("startshoot", "zoom", zoomTextBox.Text);
            }
            MessageBox.Show(res, "Start shoot message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Common.writeToLog(String.Format("Start shoot message: {0}", res));
        }

        private void StopShootButton_Click(object sender, EventArgs e)
        {
            string res = Common.sendQuery("stopshoot");
            MessageBox.Show(res, "Stop shoot message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Common.writeToLog(String.Format("Start shoot message: {0}", res));
        }
        #endregion

        private void deleteDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete the database? ", "Deleting", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.OK)
                {
                    ImgSetServer source = new ImgSetServer();
                    source.truncate();
                    Common.writeToLog("ImageProcessingControl: DataBase and AUVSI files has been deleted");
                }
            }
            catch (Exception ex)
            {
                Common.writeToLog(String.Format("ImageProcessingConrol: deleteDBToolStripMenuItem_Click was failed - Exception was thrown {0}", ex.Message));
                MessageBox.Show(string.Format("ImageProcessingConrol: deleteDBToolStripMenuItem_Click was failed: Exception was thrown - {0}", ex.Message), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteDBAUVSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete the database and the files in AUVSI directory? ", "Deleting", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.OK)
                {
                    ImgSetServer source = new ImgSetServer();
                    source.delete();
                    Common.writeToLog("ImageProcessingControl: DataBase and AUVSI files has been deleted");
                }
            }
            catch (Exception ex)
            {
                Common.writeToLog(String.Format("ImageProcessingConrol: deleteDBAUVSToolStripMenuItem_Click was failed - Exception was thrown {0}", ex.Message));
                MessageBox.Show(string.Format("ImageProcessingConrol: deleteDBAUVSToolStripMenuItem_Click was failed: Exception was thrown - {0}", ex.Message), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImageProcessingDialog_MouseMove(object sender, MouseEventArgs e)
        {
            if (upperMenuStrip.Bounds.Contains(e.Location) && !upperMenuStrip.Visible)
            {
                upperMenuStrip.Show();
            }
            if (upperMenuStrip.Visible && !(upperMenuStrip.Bounds.Contains(e.Location) || deleteDBToolStripMenuItem.Bounds.Contains(e.Location) || deleteDBAUVSToolStripMenuItem.Bounds.Contains(e.Location)))
            {
                upperMenuStrip.Visible = false;
            }
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (TabControl.SelectedTab == TabControl.TabPages["ImagesTabPage"])
            {
                ShowImageAndInfoInForm(ImageListView);
            }
            else if (TabControl.SelectedTab == TabControl.TabPages["CropsTabPage"])
            {
                ShowImageAndInfoInForm(CropListView);
            }
            else
            {
                ShowImageAndInfoInForm(TrueTargetsListView);
            }
        }

        private void SetIsRealTaregtInCropListView(int index, string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (CropListView.InvokeRequired)
            {
                SetIsRealTaregtInCropListViewCallback d = new SetIsRealTaregtInCropListViewCallback(SetIsRealTaregtInCropListView);
                this.Invoke(d, new object[] { index, text });
            }
            else
            {
                this.CropListView.Items[index].SubItems.Add(text);
            }
        }


        private void SetLonAndLatInListCropView(int index, string lat, string lon)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (CropListView.InvokeRequired)
            {
                SetLonAndLatInListCropViewCallback d = new SetLonAndLatInListCropViewCallback(SetLonAndLatInListCropView);
                this.Invoke(d, new object[] { index, lat,lon });
            }
            else
            {
                this.CropListView.Items[index].SubItems[2].Text = lat;
                this.CropListView.Items[index].SubItems[3].Text = lon;
            }
        }


        

        private void AddTrueTarget( TargetImage target,Image smallTrueImage,Image largeTrueImage)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (TrueTargetsListView.InvokeRequired)
            {
                AddTrueTargetCallBack d = new AddTrueTargetCallBack(AddTrueTarget);
                this.Invoke(d, new object[] { target , smallTrueImage, largeTrueImage});
            }
            else
            {
                ListViewItem item = new ListViewItem();
                int i = TrueTargetsListView.Items.Count;
                item.ImageIndex = i;
                LargeTrueImageList.Images.Add(largeTrueImage);
                SmallTrueImageList.Images.Add(smallTrueImage);
                this.TrueTargetsListView.Items.Add(target.imageName,item.ImageIndex);
                TrueTargetsListView.Items[i].Tag = target;
                TrueTargetsListView.Items[i].SubItems.Add(target.dateTime.ToString());
                TrueTargetsListView.Items[i].SubItems.Add(target.targetLatitudeDeg.ToString());
                TrueTargetsListView.Items[i].SubItems.Add(target.targetLongitudeDeg.ToString());
                TrueTargetsListView.Items[i].SubItems.Add(target.sourceFile);
                TrueTargetsListView.Items[i].SubItems.Add(target.isRealTarget.ToString());
            }
        }


        private void InitHomeGpsButton_Click(object sender, EventArgs e)
        {
            try
            {
                string GPSPointString = Common.sendQuery("getgps");
                string[] GPSCoors = GPSPointString.Split(',');
                HomeAlt = float.Parse(GPSCoors[0]);
                HomeLat = float.Parse(GPSCoors[1]);
                HomeLon = float.Parse(GPSCoors[2]);
                HomeLatGoogle = Convert.ToDouble(GPSCoors[1]);
                HomeLonGoogle = Convert.ToDouble(GPSCoors[2]);
            }
            catch (Exception ex)
            {
                Common.writeToLog(String.Format("ImageProcessingConrol: InitHomeGpsButton_click was failed - Exception was thrown {0}", ex.Message));
                MessageBox.Show(string.Format("ImageProcessingConrol: InitHomeGpsButton_click was failed: Exception was thrown - {0}", ex.Message), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        #region TrueTargetsContextMenuStrip events
        private void TrueTargetsLargeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrueTargetsListView.View = View.LargeIcon;
        }

        private void TrueTargetsSmallIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrueTargetsListView.View = View.List;
        }

        private void TrueTargetsDetailesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TrueTargetsListView.View = View.Details;
        }

        private void TrueTargetsContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            DisableDeleteInContextMenuStrip(TrueTargetsListView, TrueTargetsContextMenuStrip);
        }

        private void TrueTargetsDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteImages(TrueTargetsListView);
        }

        #endregion






    }

 public delegate void AddImageDelegate();
 public delegate void DoPingDelegate();
}

