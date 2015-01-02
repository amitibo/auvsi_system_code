using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcessingControl
{
    class GPSThreadInfo
    {
        public string cropName { get; set; }
        public float sourceYaw { get; set; }
        public float sourcePitch { get; set; }
        public float sourceRoll { get; set; }
        public float LatHome { get; set; }
        public float LongHome { get; set; }
        public float AltHome { get; set; }
        public float LatSource { get; set; }
        public float LongSource { get; set; }
        public float AltSouce { get; set; }
        public int xPixel { get; set; }
        public int yPixel { get; set; }

        public GPSThreadInfo(string _cropName, float _sourceYaw, float _sourcePitch, float _sourceRoll, float _LatHome, float _LongHome, float _AltHome, float _LatSource, float _LongSource, float _AltSouce, int _xPixel, int _yPixel)
        {
            cropName = _cropName;
            sourceYaw = _sourceYaw;
            sourcePitch = _sourcePitch;
            sourceRoll = _sourceRoll;
            LatHome = _LatHome;
            LongHome = _LongHome;
            AltHome = _AltHome;
            LatSource = _LatSource;
            LongSource = _LongSource;
            AltSouce = _AltSouce;
            xPixel = _xPixel;
            yPixel = _yPixel;

        }

    }
}
