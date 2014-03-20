using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace vidplay.kelas
{
    class randompoint
    {
        public static Point[] buattitik(int jumlah, int _originX,int _originY,int _radius)
        {
            Random rnd = new Random();
            Point[] hasil = new Point[jumlah];
            for (int i = 0; i < jumlah; i++)
            {
                var angle = rnd.NextDouble() * Math.PI * 2;
                var radius = rnd.NextDouble() * _radius;
                var x = _originX + radius * Math.Cos(angle);
                if (x < 0)
                {
                    x = 0;
                }
                var y = _originY + radius * Math.Sin(angle);
                if (y < 0)
                {
                    y = 0;
                }
                hasil[i] = new Point((int)x, (int)y);
            }
            return hasil;
        }
        public static Point[] buattitik(int jumlah, int _originX, int _originY, int _radius,Size size)
        {
            int wit = 320-size.Width; int hei = 240-size.Height;
            Random rnd = new Random();
            Point[] hasil = new Point[jumlah];
            for (int i = 0; i < jumlah; i++)
            {
                var angle = rnd.NextDouble() * Math.PI * 2;
                var radius = rnd.NextDouble() * _radius;
                var x = _originX + radius * Math.Cos(angle);
                if (x < 0)
                {
                    x = 0;
                }
                else if (x > wit)
                {
                    x = wit;
                }
                var y = _originY + radius * Math.Sin(angle);
                if (y < 0)
                {
                    y = 0;
                }
                else if (y > hei)
                {
                    y = hei;
                }
                hasil[i] = new Point((int)x, (int)y);
            }
            return hasil;
        }
        public static Point[] buattitik(int jumlah, int _originX, int _originY, int _radius, Size size,int lebar, int panjang)
        {
            int wit = lebar - size.Width; int hei = panjang - size.Height;
            Random rnd = new Random();
            Point[] hasil = new Point[jumlah];
            for (int i = 0; i < jumlah; i++)
            {
                var angle = rnd.NextDouble() * Math.PI * 2;
                var radius = rnd.NextDouble() * _radius;
                var x = _originX + radius * Math.Cos(angle);
                if (x < 0)
                {
                    x = 0;
                }
                else if (x > wit)
                {
                    x = wit;
                }
                var y = _originY + radius * Math.Sin(angle);
                if (y < 0)
                {
                    y = 0;
                }
                else if (y > hei)
                {
                    y = hei;
                }
                hasil[i] = new Point((int)x, (int)y);
            }
            return hasil;
        }
    }
}
