using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NS_OpenCV;

namespace ImageProcessingControl
{
    public partial class ImageZoomCrop : Form
    {

        int countPoints = 0;
        Point p1, p2;
        private String fileName;
        private bool IsDoubleClick = true;
        private ImageFromServer imgFromServer;
        private bool isImage;
        private Size oldSize;
       // private double xRatio, yRatio;
        private List<Rectangle> drawnRects = new List<Rectangle>();


        public ImageZoomCrop(String fileName, bool isDoubleClick, ImageFromServer sourceImg, bool isImage)
        {
            InitializeComponent();
            this.Text = sourceImg.imageName;
            pbImage.MouseWheel += new MouseEventHandler(pbImage_MouseWheel);
            pbImage.MouseHover += new EventHandler(pbImage_MouseHover);
            pbImage.MouseLeave += new EventHandler(pbImage_MouseLeave);
            this.fileName = fileName;
            this.IsDoubleClick = isDoubleClick;
            this.imgFromServer = sourceImg;
            this.isImage = isImage;
            System.Reflection.PropertyInfo pInfo = this.pbImage.GetType().GetProperty("ImageRectangle", System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Rectangle imageRect = (Rectangle)pInfo.GetValue(this.pbImage, null);
            this.oldSize = imageRect.Size;
            //this.xRatio = 1;
            //this.yRatio = 1;
        }


        private void pbImage_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Cross;
            pbImage.Focus();
        }

        private void pbImage_MouseWheel(object sender, EventArgs e)
        {
            MessageBox.Show("Unhandled Exception! :O");
        }

        private void pbImage_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void pbImage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //click position is with respect to the picturebox
            System.Reflection.PropertyInfo pInfo = this.pbImage.GetType().GetProperty("ImageRectangle", System.Reflection.BindingFlags.Public
                   | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Rectangle imageRect = (Rectangle)pInfo.GetValue(this.pbImage, null);
            if (IsDoubleClick)
            {
                if (countPoints % 2 == 0)
                {
                    p1.X = (int)((e.X - imageRect.X) * (double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalWidth"]) / imageRect.Width));
                    p1.Y = (int)((e.Y - imageRect.Y) * (double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalHeight"]) / imageRect.Height));

                    draw_point(p1.X, p1.Y);
                    countPoints++;
                    return;
                }

                if (countPoints % 2 == 1)
                {
                    p2.X = (int)((e.X - imageRect.X) * (double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalWidth"]) / imageRect.Width));
                    p2.Y = (int)((e.Y - imageRect.Y) * (double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalHeight"]) / imageRect.Height));
                    draw_point(p2.X, p2.Y);
                    countPoints++;

                    //draw the rectangle
                    draw_rec(p1, p2);
                }
            }



        }

        private static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img, img.Size);

            try
            {
                Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);
                return (Image)(bmpCrop);
            }
            catch (System.OutOfMemoryException e)
            {
                string errorMessage = String.Format("ImageZoomCrop: cropImage was failed - Exception was thrown {0}", e.Message);
                Common.writeToLog(errorMessage);
                MessageBox.Show(errorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.ArgumentException e)
            {
                string errorMessage = String.Format("ImageZommCrop: cropImage was failed - Exception was thrown {0}", e.Message);
                Common.writeToLog(errorMessage);
                MessageBox.Show(errorMessage, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        private Rectangle pointsToRectangle(Point p1, Point p2)
        {
            //int x, y, xCenter, yCenter;
            int x1, y1, x2, y2;
            double d = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
            //x = Math.Min(p1.X, p2.X);
            //y = Math.Min(p1.Y, p2.Y);
            //xCenter = Math.Max(p1.X, p2.X);
            //yCenter = Math.Max(p1.X, p2.X);
            x1 = p2.X - (int)(d / Math.Sqrt(2));
            y1 = p2.Y - (int)(d / Math.Sqrt(2));
            x2 = p2.X + (int)(d / Math.Sqrt(2));
            y2 = p2.Y + (int)(d / Math.Sqrt(2));
            Rectangle rect = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            return rect;
        }

        private void draw_rec(Point p1, Point p2)
        {
            System.Reflection.PropertyInfo pInfo = this.pbImage.GetType().GetProperty("ImageRectangle", System.Reflection.BindingFlags.Public
                   | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Rectangle imageRect = (Rectangle)pInfo.GetValue(this.pbImage, null);

            //Rectangle with respect to the original image
            Rectangle rectOriginalSize = pointsToRectangle(p1, p2);
            if (rectOriginalSize.X + rectOriginalSize.Width > double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalWidth"]))
                return;
            if (rectOriginalSize.Y + rectOriginalSize.Height > double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalHeight"]))
                return;
            drawnRects.Add(rectOriginalSize);

            int x, y, width, height;
            double xRatio = (double)imageRect.Width / double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalWidth"]);
            double yRatio = (double)imageRect.Height / double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalHeight"]);

            x = (int)(rectOriginalSize.X * xRatio) + imageRect.X;
            y = (int)(rectOriginalSize.Y * yRatio) + imageRect.Y;
            width = (int)(rectOriginalSize.Width * xRatio);
            height = (int)(rectOriginalSize.Height * yRatio);

            //rectangle with respect to the window
            Rectangle rectWindowSize = new Rectangle(x, y, width, height);

            //draw rectangle in the window.
            Graphics g = Graphics.FromHwnd(pbImage.Handle);
            using (var p = new Pen(Color.White, 1))
            {
                g.DrawRectangle(p, rectWindowSize);
            }
            g.Dispose();
            this.Refresh();

            //save the cropped rectangle
            System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);

            double xScaledRatio = double.Parse(System.Configuration.ConfigurationManager.AppSettings["ScaledDownWidth"]) / double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalWidth"]);
            double yScaledRatio = double.Parse(System.Configuration.ConfigurationManager.AppSettings["ScaledDownHeight"]) / double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalHeight"]);

            int xScaled = (int)(rectOriginalSize.X * xScaledRatio);
            int yScaled = (int)(rectOriginalSize.Y * yScaledRatio);
            int widthScaled = (int)(rectOriginalSize.Width * xScaledRatio);
            int heightScaled = (int)(rectOriginalSize.Height * yScaledRatio);
            Rectangle rectScaledDown = new Rectangle(xScaled,yScaled,widthScaled,heightScaled);

            using (Image croppedImage = cropImage(img, rectScaledDown))
            {
                string[] tempName = imgFromServer.sourceFile.Replace('/', '.').Replace('\\', '.').Split('.');
                string cropName = tempName[tempName.Length - 2];
                croppedImage.Save("C:\\AUVSI\\hand_crops\\" + cropName + "_p_" + rectScaledDown.X.ToString() + "," + rectScaledDown.Y.ToString() + "," + rectScaledDown.Width.ToString() + "," + rectScaledDown.Height.ToString() + ".png");
            }

            using (OpenCvCLR opencv = new OpenCvCLR())
            {
                float homeLat = float.Parse(System.Configuration.ConfigurationManager.AppSettings["homeLatitude"]);
                float homeLong = float.Parse(System.Configuration.ConfigurationManager.AppSettings["homeLongitude"]);
                float homeAlt = float.Parse(System.Configuration.ConfigurationManager.AppSettings["homeAltitude"]);
                string[] result = opencv.runGPSCoords_CLR((float)imgFromServer.gYaw, 0.0f, 0.0f, homeLat, homeLong, homeAlt, (float)imgFromServer.imageLatitude, (float)imgFromServer.imageLongitude, (float)imgFromServer.imageAltitude, p2.X, p2.Y);
                string fileContent = result[2] + "," + result[3] + "\n" + result[0] + "," + result[1]; //TODO - all pram 
                System.IO.File.WriteAllText("C:\\AUVSI\\hand_crops\\" + imgFromServer.imageName + "_p_" + rectScaledDown.X.ToString() + "," + rectScaledDown.Y.ToString() + "," + rectScaledDown.Width.ToString() + "," + rectScaledDown.Height.ToString() + ".txt", fileContent);
            }

        }

        private void draw_point(int X, int Y)
        {
            Graphics g = Graphics.FromHwnd(pbImage.Handle);
            SolidBrush brush = new SolidBrush(Color.White);
            Point dPoint = new Point(X, Y);
            Rectangle rect = new Rectangle(dPoint, new Size(1, 1));
            g.FillRectangle(brush, rect);
            g.Dispose();
        }

        private void pbImage_Paint(object sender, PaintEventArgs e)
        {
            if (this.isImage)
            {
                System.Reflection.PropertyInfo pInfo = this.pbImage.GetType().GetProperty("ImageRectangle", System.Reflection.BindingFlags.Public
                    | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                Rectangle imageRect = (Rectangle)pInfo.GetValue(this.pbImage, null);


                //draw the mser results
                foreach (TargetImage targetImg in imgFromServer.cropsInImage)
                {
                    //choose the rectangle color
                    Color rectColor = Color.Blue;
                    if (targetImg.isChecked == true)
                    {
                        if (targetImg.isRealTarget == 1)
                        {
                            rectColor = Color.LawnGreen;
                        }
                        else if (targetImg.isRealTarget == 0)
                        {
                            rectColor = Color.Red;
                        }
                        else
                        {
                            rectColor = Color.Orange;
                        }
                    }

                    //find the rectangle properties
                    Rectangle r = pointCordsToRectangle(targetImg.PointCords);

                    //scale the rectangle from original size to window size
                    Rectangle r2 = rectToWindowSize(r, imageRect, "ScaledDown");

                    //draw the rectangle
                    using (var p = new Pen(rectColor, 1))
                    {
                        e.Graphics.DrawRectangle(p, r2);
                    }
                }
                
                //draw the rectangles that we drew manually
                foreach (Rectangle rect in drawnRects)
                {
                    Rectangle rectWindowSize = rectToWindowSize(rect, imageRect, "orignal");
                   
                    using (var p = new Pen(Color.White, 1))
                    {
                        e.Graphics.DrawRectangle(p, rectWindowSize);
                    }
                }
            }
        }

        private Rectangle rectToWindowSize(Rectangle rect, Rectangle imageRect, string s)
        {
            int x, y, width, height;
            string w = s + "Width";
            string h = s + "Height";
            double xRatio = (double)imageRect.Width / double.Parse(System.Configuration.ConfigurationManager.AppSettings[w]);
            double yRatio = (double)imageRect.Height / double.Parse(System.Configuration.ConfigurationManager.AppSettings[h]);

            x = (int)(rect.X * xRatio) + imageRect.X;
            y = (int)(rect.Y * yRatio) + imageRect.Y;
            width = (int)(rect.Width * xRatio);
            height = (int)(rect.Height * yRatio);

            return new Rectangle(x, y, width, height);
        }

        private Rectangle pointCordsToRectangle(Point[] PointCords)
        {
            int x = PointCords[0].X;
            int y = PointCords[0].Y;
            int width = 0;
            int height = 0;
            for (int i = 1; i < 4; i++)
            {
                if (x > PointCords[i].X)
                {
                    x = PointCords[i].X;
                }
                if (y > PointCords[i].Y)
                {
                    y = PointCords[i].Y;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (width < PointCords[i].X - x)
                {
                    width = PointCords[i].X - x;
                }
                if (height < PointCords[i].Y - y)
                {
                    height = PointCords[i].Y - y;
                }
            }

            return new Rectangle(x, y, width, height);
        }
    }
}
