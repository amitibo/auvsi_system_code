using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using System.Net;

namespace ImageProcessingControl
{
    public static class Common
    {
        public static void writeToLog(string Message)
        {
            DateTime time = DateTime.Now;

            string logPath = System.Configuration.ConfigurationManager.AppSettings["LogPath"] + "-" + time.Day + "-" + time.Month + "-" + time.Year + ".log";

            string message = String.Format("{0}: {1}", time, Message);

            File.AppendAllText(logPath, message + Environment.NewLine);

        }

        public static string sendQuery(string addr = "", string param = "", string value = "", string host = "http://192.168.1.12:8080/")
        {
	        WebClient webClient = new WebClient();
	        if(param.Length > 0)
	        {
		        webClient.QueryString.Add(param, value);
	        }
	        string result = webClient.DownloadString(host + addr);
	        return result;
        }

        public static void sendQueryAsync(string addr = "", string param = "", string value = "", string host = "http://192.168.1.12:8080/")
        {
	        WebClient webClient = new WebClient();
	        if(param.Length > 0)
	        {
		        webClient.QueryString.Add(param, value);
	        }
            Uri hostAndAdds = new Uri(host + addr);
            webClient.DownloadStringAsync(hostAndAdds);
        }

        public static void drawMserResults(PaintEventArgs e,ImageFromServer imgFromServer, double x_ratio, double y_ratio, int xTranslation, int yTranslation)
        {
            foreach (TargetImage targetImg in imgFromServer.cropsInImage)
            {
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

                int x = targetImg.PointCords[0].X;
                int y = targetImg.PointCords[0].Y;
                int width = 0;
                int height = 0;
                for (int i = 1; i < 4; i++)
                {
                    if (x > targetImg.PointCords[i].X)
                    {
                        x = targetImg.PointCords[i].X;
                    }
                    if (y > targetImg.PointCords[i].Y)
                    {
                        y = targetImg.PointCords[i].Y;
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (width < targetImg.PointCords[i].X - x)
                    {
                        width = targetImg.PointCords[i].X - x;
                    }
                    if (height < targetImg.PointCords[i].Y - y)
                    {
                        height = targetImg.PointCords[i].Y - y;
                    }
                }

                //System.Reflection.PropertyInfo pInfo = pb.GetType().GetProperty("ImageRectangle", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                //Rectangle rectangle = (Rectangle)pInfo.GetValue(pb, null);

                x = (int)(x * x_ratio)+xTranslation;
                y = (int)(y * y_ratio)+yTranslation;
                width = (int)(width * x_ratio);
                height = (int)(height * y_ratio);
                Rectangle drawRect = new Rectangle(x, y, width, height);
                using (var p = new Pen(rectColor, 1))
                {
                    e.Graphics.DrawRectangle(p, drawRect);
                }
                //System.Drawing.Image img = System.Drawing.Image.FromFile(imgFromServer.sourceFile);
                //Size original = img.Size;
                //float x_ratio = (float)original.Width / pbImage.Size.Width;
                //float y_ratio = (float)original.Height / pbImage.Size.Height;
                //Rectangle cropRect = new Rectangle((int)(x * x_ratio), (int)(y * y_ratio), (int)(width * x_ratio), (int)(height * y_ratio));
                //Image croppedImage = cropImage(img, cropRect);
                //string cropName = imgFromServer.sourceFile.Split('\\')[1].Split('/')[1].Split('.')[0];
                //croppedImage.Save("C:\\AUVSI\\mser-outputs\\" + cropName + ".jpg");
                //!!!!!!!---------------TODO: generate a filename
            }

        }
    }
}
