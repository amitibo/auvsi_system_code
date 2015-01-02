using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageProcessingControl
{
    public class TargetImage : ImageFromServer
    {
        #region private properties

        private string TargetColor = String.Empty;
        private string LetterColor = String.Empty;
        private double TargetLatitudeDeg;
        private double TargetLongitudeDeg;
        private string TargetLatitudeMinute;
        private string TargetLongitudeMinute;
        private string Letter = String.Empty;
        private string TargetOrientation = String.Empty;
        private string Shape = String.Empty;
        private string Orientation = String.Empty;
        private int IsRealTarget = 0;
        private bool IsChecked = false;
        public Point[] PointCords = new Point[4];
        public int indexListView;
        
        #endregion

        #region public members


        public int isRealTarget
        {
            get
            {
                return IsRealTarget;
            }
            set
            {
                IsRealTarget = value;
            }
        }

        public bool isChecked
        {
            get
            {
                return IsChecked;
            }
            set
            {
                IsChecked = value;
            }
        }

        public string targetColor
        {
            get
            {
                return TargetColor;
            }
            set
            {
                TargetColor = value;
            }
        }

        public string orientation
        {
            get
            {
                return Orientation;
            }
            set
            {
                Orientation = value;
            }
        }

        public string letterColor
        {
            get
            {
                return LetterColor;
            }
            set
            {
                LetterColor = value;
            }
        }

        public double targetLatitudeDeg
        {
            get
            {
                return TargetLatitudeDeg;
            }
            set
            {
                TargetLatitudeDeg = value;
            }
        }

        public double targetLongitudeDeg
        {
            get
            {
                return TargetLongitudeDeg;
            }
            set
            {
                TargetLongitudeDeg = value;
            }
        }

        public string targetLatitudeMinute
        {
            get
            {
                return TargetLatitudeMinute;
            }
            set
            {
                TargetLatitudeMinute = value;
            }
        }

        public string targetLongitudeMinute
        {
            get
            {
                return TargetLongitudeMinute;
            }
            set
            {
                TargetLongitudeMinute = value;
            }
        }

        public string letter
        {
            get
            {
                return Letter;
            }
            set
            {
                Letter = value;
            }
        }

        public string targetOrientation
        {
            get
            {
                return TargetOrientation;
            }
            set
            {
                TargetOrientation = value;
            }
        }

        public string shape
        {
            get
            {
                return Shape;
            }
            set
            {
                Shape = value;
            }
        }
        #endregion

        #region constructors

        public TargetImage(DateTime _dateTime, double _imageLatitude, double _imageLongitude, double _imageAltitude, string _soureFile, string _imageName, double _gRoll, double _gPitch, double _gYaw, Point[] cords)
            : base(_dateTime, _imageLatitude, _imageLongitude, _imageAltitude, _soureFile, _imageName, _gRoll, _gPitch, _gYaw)
        {
            double factorX = double.Parse(System.Configuration.ConfigurationManager.AppSettings["ScaledDownWidth"]) / double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalWidth"]);
            double factorY = double.Parse(System.Configuration.ConfigurationManager.AppSettings["ScaledDownHeight"]) / double.Parse(System.Configuration.ConfigurationManager.AppSettings["orignalHeight"]);
            for (int i = 0; i < 4; i++)
            {
                PointCords[i] = new Point((int)(factorX * cords[i].X), (int)(factorY * cords[i].Y));
            }
        }


        //public TargetImage(DateTime _dateTime, double _imageLatitude, double _imageLongitude, string _soureFile, string _imageName, double _gRoll,double _gPitch, double _gYaw)
        //    : base(_dateTime, _imageLatitude, _imageLongitude, _soureFile, _imageName, _gRoll,_gPitch,_gYaw) { }
       
        #endregion
    }

}
