using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET;
using System.Threading;

namespace ImageProcessingControl
{
    public partial class GoogleMapDialog : Form
    {
        //long offset = 0;
        GMapOverlay markersOverlay = new GMapOverlay("markers");
        public BlockingCollection<PointLatLng> points = new BlockingCollection<PointLatLng>();
       // GMapPolygon polygon;
        //GMapMarkerImage marker;
        private static Mutex mut = new Mutex();
        

        public GoogleMapDialog(BlockingCollection<PointLatLng> markers)
        {
            InitializeComponent();
            foreach (PointLatLng P in markers)
            {
                points.Add(P);
            }
        }

        private void gMapControl_Load(object sender, EventArgs e)
        {
            //Initialize map
            gMapControl.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            gMapControl.MapProvider = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.CacheOnly;

            PointLatLng home = new PointLatLng(ImageProcessingDialog.HomeLatGoogle, ImageProcessingDialog.HomeLonGoogle);

           gMapControl.Position = home;

            //gMapControl.Position = new GMap.NET.PointLatLng(38.1453274, -76.4286482);
            
            mut.WaitOne();
            GMarkerGoogle HomeMarker = new GMarkerGoogle(home, GMarkerGoogleType.red);
            markersOverlay.Markers.Add(HomeMarker);

            
            mut.ReleaseMutex();

            //gMapControl.Position = new GMap.NET.PointLatLng(38.1453274, -76.4286482);
            gMapControl.DragButton = MouseButtons.Left;
            gMapControl.CanDragMap = true;
            gMapControl.ShowCenter = false;
            
            foreach (PointLatLng point in points )
            {
                addMarker(point);
            }

            mut.WaitOne();
            gMapControl.Overlays.Add(markersOverlay);
            mut.ReleaseMutex();

            //gMapControl.Overlays.Add(markersOverlay);
        }


        public void addMarker(PointLatLng point)
        {
            mut.WaitOne();
            GMarkerGoogle marker = new GMarkerGoogle(point,  GMarkerGoogleType.blue);
            gMapControl.Position = point;
            markersOverlay.Markers.Add(marker);
           
            //gMapControl.Overlays.Add(markersOverlay);
            mut.ReleaseMutex();
        }


        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            //Double latitude;
            //Double longitude;

            //try
            //{
            //    using (FileStream file = File.Open(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.Write))
            //    {
            //        using (StreamReader reader = new StreamReader(file))
            //        {

            //            if (offset == 0)
            //            {
            //                polygon = new GMapPolygon(points, "mypolygon");
            //                polygon.Stroke = new Pen(Color.Blue, 3);
            //                polyOverlay.Polygons.Add(polygon);
            //                gMapControl.Overlays.Add(polyOverlay);
            //                gMapControl.Overlays.Add(markersOverlay);
            //                Image markerImage = Image.FromFile("C:\\Users\\Liat\\Desktop\\Image Processing Folder\\airplane-icon40.png");
            //                marker = new GMapMarkerImage(new PointLatLng(), markerImage);
            //                markersOverlay.Markers.Add(marker);
            //            }

            //                file.Seek(offset, SeekOrigin.Begin);
            //                if (!reader.EndOfStream)
            //                {
            //                    do
            //                    {

            //                        string pointFromFile = reader.ReadLine();

            //                        string[] pointCoordinates = pointFromFile.Split(',');
            //                        latitude = Double.Parse(pointCoordinates[0]);
            //                        longitude = Double.Parse(pointCoordinates[1]);

            //                        PointLatLng point = new PointLatLng(latitude, longitude);

            //                        polygon.Points.Add(point);

            //                    } while (!reader.EndOfStream);

            //                    offset = file.Position;


            //                    gMapControl.Position = new GMap.NET.PointLatLng(latitude, longitude);
            //                    marker.Position = new PointLatLng(latitude, longitude);
            //                    gMapControl.Zoom = 18;
            //                    //Image markerImage = Image.FromFile("C:\\Users\\Liat\\Desktop\\Image Processing Folder\\airplane-icon40.png");
            //                    //GMapMarkerImage marker = new GMapMarkerImage(new PointLatLng(latitude,longitude), markerImage);
            //                    //markersOverlay.Markers.Add(marker);
            //                    //polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));

            //                }

            //        }
                }

        private void GoogleMapDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            ImageProcessingDialog.googleIsRunning = false;
        }


                //List<PointLatLng> points = new List<PointLatLng>();

                //for (int i = 0; i < 4; i++ )
                //{
                //string[] point = p.Split(',');
                //points.Add(new PointLatLng(38.1453274, -76.4286482));
                //points.Add(new PointLatLng(38.1454374, -76.4287382));
                //points.Add(new PointLatLng(38.1455474, -76.4288282));
                //points.Add(new PointLatLng(38.1456574, -76.4289182));
                //}
                //string[] position = ListPoints.Items[0].ToString().Split(',');
                //gMapControl.Position = new GMap.NET.PointLatLng(38.1453274, -76.4286482);
                //Image markerImage = Image.FromFile("C:\\Users\\Liat\\Desktop\\Image Processing Folder\\airplane-icon40.png");
                //GMapMarkerImage marker = new GMapMarkerImage(new PointLatLng(38.1453274, -76.4286482), markerImage);
                //markersOverlay.Markers.Add(marker);
                //GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
                //polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
                //polygon.Stroke = new Pen(Color.Red, 1);
                //polyOverlay.Polygons.Add(polygon);
                //gMapControl.Overlays.Add(polyOverlay);
                //gMapControl.Overlays.Add(markersOverlay);


                //using ( StreamReader reader = new StreamReader(file)  )
                //{
                //    if (offset == 0)
                //    {
                //        //TODO initialize google path
                //    }
                //    else
                //    {
                //        file.Seek(offset, SeekOrigin.Begin);
                //        if (!reader.EndOfStream)
                //        {
                //            do
                //            {
                //                Console.WriteLine(reader.ReadLine());
                //            } while (!reader.EndOfStream);

                //            offset = file.Position;
                //        }
                //    }

                //}
            //}
            //catch (Exception ex)
            //{
            //    Common.writeToLog(String.Format("GoogleMapDialog: fileSystemWatcher_Changed was failed - Exception was thrown {0}", ex.Message));
            //    MessageBox.Show(string.Format("GoogleMapDialog: fileSystemWatcher_Changed was failed - Exception was thrown {0}", ex.Message), "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        //}
    }
}
