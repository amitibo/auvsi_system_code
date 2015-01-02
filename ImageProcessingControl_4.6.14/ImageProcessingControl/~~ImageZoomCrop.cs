using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        private double xRatio, yRatio;
        private List<Rectangle> drawnRects = new List<Rectangle>();


        public ImageZoomCrop(String fileName,bool isDoubleClick, ImageFromServer sourceImg, bool isImage)
        {
            InitializeComponent();
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
            this.xRatio = 1;
            this.yRatio = 1;
            //if (this.isImage)
            //{
            //    drawMserResults();
            //}
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
            System.Reflection.PropertyInfo pInfo = this.pbImage.GetType().GetProperty("ImageRectangle", System.Reflection.BindingFlags.Public
                   | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Rectangle imageRect = (Rectangle)pInfo.GetValue(this.pbImage, null);
            if (IsDoubleClick)
            {
                if (countPoints%2 == 0)
                {
                    p1.X = (e.X - this.pbImage.Width) * (imageRect.Width / this.pbImage.Size.Width);
                    p1.Y = (e.Y - this.pbImage.Height) * (imageRect.Height / this.pbImage.Size.Height);
                    draw_point(p1.X, p1.Y);
                    countPoints++;
                    return;
                }

                if (countPoints%2 == 1)
                {
                    p2.X = (e.X - this.pbImage.Width) * (imageRect.Width / this.pbImage.Size.Width);
                    p2.Y = (e.Y - this.pbImage.Height) * (imageRect.Height / this.pbImage.Size.Height);
                    draw_point(p2.X, p2.Y);
                    countPoints++;

                    //draw the rectangle
                    draw_rec(p1, p2);


                    return;
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
                string errorMessage = String.Format("ImageZommCrop: cropImage was failed - Exception was thrown {0}", e.Message); 
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


        private void draw_rec(Point p1, Point p2)
        {
            System.Reflection.PropertyInfo pInfo = this.pbImage.GetType().GetProperty("ImageRectangle", System.Reflection.BindingFlags.Public
                   | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Rectangle imageRect = (Rectangle)pInfo.GetValue(this.pbImage, null);

            double xRatio = (double)imageRect.Width / this.pbImage.Size.Width;
            double yRatio = (double)imageRect.Height / this.pbImage.Size.Height;

            int x, y, width, height;
            int xCenter, yCenter, x1, y1, x2, y2;
            double d = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y,2));
            x = Math.Min(p1.X, p2.X);
            y = Math.Min(p1.Y, p2.Y);
            xCenter = Math.Max(p1.X, p2.X);
            yCenter = Math.Max(p1.X, p2.X);
            x1 = p2.X - (int)(d / Math.Sqrt(2));
            y1 = p2.Y - (int)(d / Math.Sqrt(2));
            x2 = p2.X + (int)(d / Math.Sqrt(2));
            y2 = p2.Y + (int)(d / Math.Sqrt(2));

            /* Check if the selection is out of range */
            if (x1 > pbImage.Size.Width || x2 > pbImage.Size.Width
                || y1 > pbImage.Size.Height || y2 > pbImage.Size.Height)
            {
                return;
            }

            width = x2 - x1;
            height = y2 - y1;

            //draw the rectangle ביחס לתמונה בתוך הפיקצ'ר בוקס
            Rectangle drawRect = new Rectangle(x1,y1,width,height);
            Graphics g = Graphics.FromHwnd(pbImage.Handle);
            using (var p = new Pen(Color.White, 1))
            {
                g.DrawRectangle(p, drawRect);
            }
            g.Dispose();
 
            Rectangle r = new Rectangle((int)(x1*xRatio), (int)(y1*yRatio), (int)(width*xRatio), (int)(height*yRatio));
            drawnRects.Add(r);
            this.Refresh();
            //Rectangle drawRect = new Rectangle((int)(x1 * xRatio) + imageRect.X, (int)(y1 * yRatio) + imageRect.Y, (int)(width * xRatio), (int)(height * yRatio));
            //Graphics g = Graphics.FromHwnd(pbImage.Handle);
            //using (var p = new Pen(Color.White, 1))
            //{
            //    g.DrawRectangle(p, drawRect);
            //}
            //g.Dispose();
            //r is a rectangle ביחס לתמונה המקורית
            //Rectangle r = new Rectangle(drawRect.X, drawRect.Y, (int)(xRatio * drawRect.Width), (int)(yRatio * drawRect.Height));
            //drawnRects.Add(r);
            //System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);
            //Size original = img.Size;
            //double x_ratio = (double)original.Width / imageRect.Size.Width;
            //double y_ratio = (double)original.Height / imageRect.Size.Height;
            //Rectangle cropRect = new Rectangle((int)(x * x_ratio), (int)(y * y_ratio), (int)(width*x_ratio), (int)(height*y_ratio));
            
            //Image croppedImage = cropImage(img, r);
            //string[] tempName = imgFromServer.sourceFile.Replace('/', '.').Replace('\\', '.').Split('.');
            //string cropName = tempName[tempName.Length - 2];
            //croppedImage.Save("C:\\AUVSI\\crops\\" + cropName + "_p_" + x1.ToString() + "," + y1.ToString() + "," + width.ToString() + "," + height.ToString() + ".jpg");

        }


        private void drawMserResults(PaintEventArgs e)
        {
            foreach (TargetImage targetImg in imgFromServer.cropsInImage)
            {
                Color rectColor = Color.Blue;
                if (targetImg.isChecked == true)
                {
                    if (targetImg.isRealTarget == true)
                    {
                        rectColor = Color.LawnGreen;
                    }
                    else
                    {
                        rectColor = Color.Red;
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
            /*
            Int32 x, y, width, height;
            x = Math.Min(p1.X, p2.X);
            y = Math.Min(p1.Y, p2.Y);
            width = Math.Abs(p2.X - p1.X);
            height = Math.Abs(p2.Y - p1.Y);
            Rectangle drawRect = new Rectangle(x, y, width, height);
            Graphics g = Graphics.FromHwnd(pbImage.Handle);
            using (var p = new Pen(Color.Blue, 4))
            {
                g.DrawRectangle(p, drawRect);
            }
            System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);
            Size original = img.Size;
            float x_ratio = (float)original.Width / pbImage.Size.Width;
            float y_ratio = (float)original.Height / pbImage.Size.Height;
            Rectangle cropRect = new Rectangle((int)(x * x_ratio), (int)(y * y_ratio), (int)(width * x_ratio), (int)(height * y_ratio));

            Image croppedImage = cropImage(img, cropRect);
            croppedImage.Save("C:/temp/output/a.jpg");

            /*Bitmap src = pbImage.Image as Bitmap;
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);
            using (Graphics g2 = Graphics.FromImage(target))
            {
                g2.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                   cropRect,
                   GraphicsUnit.Pixel);
            }
             */


            /*target.Save("C:/output/a.jpg");
            
            Bitmap src = Image.FromFile(fileName) as Bitmap;
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using(Graphics g = Graphics.FromImage(target))
            {
                 g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height), 
                    cropRect,                        
                    GraphicsUnit.Pixel);
}
            */



        }




        private void draw_point(int X, int Y)
        {
            Graphics g = Graphics.FromHwnd(pbImage.Handle);
            SolidBrush brush = new SolidBrush(Color.LimeGreen);
            Point dPoint = new Point(X, Y);
            Rectangle rect = new Rectangle(dPoint, new Size(1, 1));
            g.FillRectangle(brush, rect);
            g.Dispose();
        }

        private void ImageZoomCrop_Load(object sender, EventArgs e)
        {

        }

        private void pbImage_Paint(object sender, PaintEventArgs e)
        {
            if (this.isImage)
            {
                System.Reflection.PropertyInfo pInfo = this.pbImage.GetType().GetProperty("ImageRectangle", System.Reflection.BindingFlags.Public
                    | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                Rectangle imageRect = (Rectangle)pInfo.GetValue(this.pbImage, null);
                double x_ratio = this.xRatio * (double)imageRect.Width / oldSize.Width;
                double y_ratio = this.yRatio * (double)imageRect.Height / oldSize.Height;

                Common.drawMserResults(e, imgFromServer, x_ratio, y_ratio, imageRect.X, imageRect.Y);
                foreach (Rectangle rect in drawnRects)
                {
                    
                    int x = (int)(rect.X * x_ratio) + imageRect.X;
                    int y = (int)(rect.Y * y_ratio) + imageRect.Y;
                    int width = (int)(rect.Width * x_ratio);
                    int height = (int)(rect.Height * y_ratio);
                    Rectangle drawRect = new Rectangle(x, y, width, height);
                    using (var p = new Pen(Color.White, 1))
                    {
                        e.Graphics.DrawRectangle(p, drawRect);
                    }
                }
            }
        }

        private void ImageZoomCrop_ResizeEnd(object sender, EventArgs e)
        {
            

            //after drawing new rectangles:
            System.Reflection.PropertyInfo pInfo = this.pbImage.GetType().GetProperty("ImageRectangle", System.Reflection.BindingFlags.Public
                    | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Rectangle imageRect = (Rectangle)pInfo.GetValue(this.pbImage, null);
            this.xRatio = this.xRatio * imageRect.Width / this.oldSize.Width;
            this.yRatio = this.yRatio * imageRect.Height / this.oldSize.Height;
            this.oldSize = imageRect.Size;
        }

        private void ImageZoomCrop_Resize(object sender, EventArgs e)
        {
            //this.oldSize = ((Control)sender).Size;
        }



    }
}
