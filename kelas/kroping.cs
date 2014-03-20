using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;


namespace vidplay.kelas
{
    class kroping
    {
        public static Bitmap Crop(Bitmap image,int xPosition, int yPosition, int width, int height)
        {
            Bitmap temp = (Bitmap)image;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (xPosition + width > image.Width)
                width = image.Width - xPosition;
            if (yPosition + height > image.Height)
                height = image.Height - yPosition;
            Rectangle rect = new Rectangle(xPosition, yPosition, width, height);
            Bitmap baru = (Bitmap)bmap.Clone(rect, bmap.PixelFormat);
            return baru;
        }

        public static Bitmap cropclone(Bitmap image, Rectangle rect, out long waktu)
        {
            Stopwatch w;
            w = Stopwatch.StartNew();
            Bitmap hasil = image.Clone(rect,image.PixelFormat);
            w.Stop();
            waktu = w.ElapsedMilliseconds;
            return hasil;
        }

        public static Bitmap cropclone(Bitmap image, Point poin, Size size, out long waktu) //
        {
            Stopwatch w;
            Rectangle rect = new Rectangle(poin, size);
            w = Stopwatch.StartNew();
            Bitmap hasil = image.Clone(rect, image.PixelFormat);
            w.Stop();
            waktu = w.ElapsedMilliseconds;
            return hasil;
        }

        public static Bitmap cropclone(Bitmap image, int x, int y, int w, int h, out long waktu)
        {
            Stopwatch wa;
            Point poin = new Point(x, y);
            Size size = new Size(w, h);
            Rectangle rect = new Rectangle(poin, size);
            wa = Stopwatch.StartNew();
            Bitmap hasil = image.Clone(rect, image.PixelFormat);
            wa.Stop();
            waktu = wa.ElapsedMilliseconds;
            return hasil;
        }

        public static Bitmap cropori(Bitmap image, Rectangle rect,Size size, out long waktu)
        {
            Stopwatch wa;
            wa = Stopwatch.StartNew();
            int centerx = Convert.ToInt16(rect.X + (0.5 * rect.Width)-(0.5*size.Width));
            int centery = Convert.ToInt16(rect.Y + (0.5 * rect.Height)-(0.5*size.Height));
            Point newpoint = new Point(centerx, centery);
            Rectangle newrect = new Rectangle(newpoint, size);
            Bitmap hasil = image.Clone(newrect, image.PixelFormat);
            wa.Stop();
            waktu = wa.ElapsedMilliseconds;
            return hasil;
        }
    }
}
