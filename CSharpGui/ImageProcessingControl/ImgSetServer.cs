using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.Drawing;

namespace ImageProcessingControl
{
    class ImgSetServer
    {
        public UInt32 id;           // Set ID
        public DateTime datetime;   // Date/Time image was taken
        public double altitude;     // Flight altitude
        public double lon;          // GPS longitude
        public double lat;          // GPS latitude
        public double gRoll;        // Gimbal roll value
        public double gPitch;       // Gimbal pitch value
        public double gYaw;         // Gimbal yaw value
        public string src;          // Big image file name
        public List<imgcrop> crops = new List<imgcrop>();   // List that contains the image crops.
        public static int tmpid = 1;

        public ImgSetServer() { }

        private bool getFromXml(string param = "")
        {
            // Sends an HTTP request to the local service and construct an imgset object with the appropiate data.
            // Input:
            // param(string) -
            //          "next"  : Returns the next unseen set (Default).
            //          "latest": Returns the latest set available.
            //          "1234"  : Returns the set with ID = 1234.
            // Output:
            // imgset object with values loaded.


            //imgset curr = new imgset();
            XmlDocument doc = new XmlDocument();
            string xml = grabPicInfo(param); 
            if(xml.Length<55)
            {
                return false;
            }
            doc.LoadXml(xml);
            this.id = Convert.ToUInt32(doc.SelectSingleNode("/AUVSI/image").Attributes["id"].Value);
            this.datetime = DateTime.ParseExact(doc.SelectSingleNode("/AUVSI/image").Attributes["time"].Value, "yyyy-MM-dd HH:mm:ss.ffffff", System.Globalization.CultureInfo.InvariantCulture);
            this.altitude = Convert.ToDouble(doc.SelectSingleNode("/AUVSI/image/location").Attributes["altitude"].Value);
            this.lon = Convert.ToDouble(doc.SelectSingleNode("/AUVSI/image/location").Attributes["lon"].Value);
            this.lat = Convert.ToDouble(doc.SelectSingleNode("/AUVSI/image/location").Attributes["lat"].Value);
            this.gPitch = Convert.ToDouble(doc.SelectSingleNode("/AUVSI/image/gimbal").Attributes["pitch"].Value);
            this.gRoll = Convert.ToDouble(doc.SelectSingleNode("/AUVSI/image/gimbal").Attributes["roll"].Value);
            this.gYaw = Convert.ToDouble(doc.SelectSingleNode("/AUVSI/image/gimbal").Attributes["yaw"].Value);
            this.src = doc.SelectSingleNode("/AUVSI/image/large_img").Attributes["src"].Value;

            XmlNodeList crops = doc.GetElementsByTagName("img");
            for (int i = 0; i < crops.Count; i++)
            {
                imgcrop tcrop = new imgcrop();
                tcrop.id = Convert.ToUInt32(crops[i].Attributes["id"].Value);
                tcrop.src = crops[i].Attributes["src"].Value;
                tcrop.cropcords[0] = new Point(Convert.ToInt32(crops[i].Attributes["C1"].Value.Split(',')[0]), Convert.ToInt32(crops[i].Attributes["C1"].Value.Split(',')[1]));
                tcrop.cropcords[1] = new Point(Convert.ToInt32(crops[i].Attributes["C2"].Value.Split(',')[0]),Convert.ToInt32(crops[i].Attributes["C2"].Value.Split(',')[1]));
                tcrop.cropcords[2] = new Point(Convert.ToInt32(crops[i].Attributes["C3"].Value.Split(',')[0]),Convert.ToInt32(crops[i].Attributes["C3"].Value.Split(',')[1]));
                tcrop.cropcords[3] = new Point(Convert.ToInt32(crops[i].Attributes["C4"].Value.Split(',')[0]),Convert.ToInt32(crops[i].Attributes["C4"].Value.Split(',')[1]));
                this.crops.Add(tcrop);
            }
            return true;
        }

        private string grabPicInfo(string param)
        {
            string result=string.Empty;
             //Once the service is up and running the next part will be un-commented.
             //At the moment reading local .xml file for testing purposes.

            WebClient webClient = new WebClient();
            webClient.QueryString.Add("action", param);
            result = webClient.DownloadString("http://localhost:8080");

            return result;
            //if (tmpid == 6)
            //    return result;


            //string results = System.IO.File.ReadAllText("C:\\temp\\xml\\output_example" + tmpid + ".xml"); // Would get commented out once the service is up and running.
            //tmpid++;

            //return results;
        }

        //Returns the latest set available.
        public bool getLatest()
        {
            return this.getFromXml("latest");
        }

        //Returns the next unseen set.
        public bool getNext()
        {
            return this.getFromXml("next");
        }

        //Returns the set with ID = 1234.
        public bool getById(int id)
        {
            return this.getFromXml(id.ToString());
        }
        
        //Uploading all Images from the begaining.
        public void reset()
        {
             this.grabPicInfo("reset");
        }

        //Deleting DB and Images from computre
        public void delete()
        {
             this.grabPicInfo("delete");
        }

        //deleting DB
        public void truncate()
        {
             this.grabPicInfo("truncate");
        }

    }

    class imgcrop
    {
        public UInt32 id;   // Crop ID
        public string src;  // Crop file name
        public Point[] cropcords = new Point[4];  // Crops image coordinates
    }


    public class NoNewData : Exception
    {
        public NoNewData()
        {
        }
    }
}
