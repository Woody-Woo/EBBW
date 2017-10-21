using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ClickerSDK.Screen
{
    public static class ScreenHelper
    {
        


        public static void GetScreen(string fileName,int x1,int y1, int x2, int y2)
        {
            Rectangle rect = new Rectangle(0, 0, x2-x1, y2-y1);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(x1, y2, x2, y2, bmp.Size, CopyPixelOperation.SourceCopy);
            bmp.Save(fileName, ImageFormat.Jpeg);
        }
    }
}