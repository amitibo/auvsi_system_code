using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessingControl
{
    public class ImageFromServer
    {
        #region private properties

        private DateTime DateTime;
        private double ImageLatitude;
        private double ImageLongitude;
        private double ImageAltitude;
        private string SourceFile;
        private string ImageName;
        private double GRoll;        
        private double GPitch;
        private double GYaw;
        public List<TargetImage> cropsInImage = new List<TargetImage>();
        #endregion

        #region public properties

        public DateTime dateTime
        {
            get
            {
                return DateTime;
            }
            set
            {
                DateTime = value;
            }
        }

        public double gRoll
        {
            get
            {
                return GRoll;
            }
            set
            {
                GRoll = value;
            }
        }

        public double gPitch
        {
            get
            {
                return GPitch;
            }
            set
            {
                GPitch = value;
            }
        }

        public double gYaw
        {
            get
            {
                return GYaw;
            }
            set
            {
                GYaw = value;
            }
        }

        public double imageLatitude
        {
            get
            {
                return ImageLatitude;
            }
            set
            {
                ImageLatitude = value;
            }
        }

        public double imageAltitude
        {
            get
            {
                return ImageAltitude;
            }
            set
            {
                ImageAltitude = value;
            }
        }

        public double imageLongitude
        {
            get
            {
                return ImageLongitude;
            }
            set
            {
                ImageLongitude = value;
            }
        }

        public string sourceFile
        {
            get
            {
                return SourceFile;
            }
            set
            {
                SourceFile = value;
            }
        }

        public string imageName
        {
            get
            {
                return ImageName;
            }
            set
            {
                ImageName = value;
            }
        }
        #endregion

        #region Constructor

        public ImageFromServer(DateTime _dateTime, double _imageLatitude, double _imageLongitude, double _imageAltitude, string _soureFile, string _imageName, double _gRoll, double _gPitch, double _gYaw)
        {
            dateTime = _dateTime;
            imageLatitude = _imageLatitude;
            imageLongitude = _imageLongitude;
            imageAltitude = _imageAltitude;
            sourceFile = _soureFile;
            imageName = _imageName;
            gRoll = _gRoll;
            gPitch = _gPitch;
            gYaw = _gYaw;
        }

        #endregion

    }
}
