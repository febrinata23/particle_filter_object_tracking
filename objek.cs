using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;

namespace vidplay
{
    class objek
    {

        public objek(int jml)
        {
            _bitmaporang = new List<Bitmap>(jml);
            _hist = new List<Histogram>(jml);
            _kotak = new List<Rectangle>(jml);
            _warna = new List<Color>(jml);
            _ukuran = new List<Size>(jml);
            _jmlpraticle = new List<double>(jml);
            _warna.Add(Color.Red);
            _warna.Add(Color.Yellow);
            _warna.Add(Color.Green);
            _warna.Add(Color.Blue);
            _warna.Add(Color.Orange);
            _warna.Add(Color.Violet);
            _warna.Add(Color.YellowGreen);
            _warna.Add(Color.Aqua);
            _warna.Add(Color.Brown);
        }

        public List<Bitmap> bitmaporang
        {
            get { return _bitmaporang; }
        }

        public List<Histogram> hist
        {
            get { return _hist; }
        }

        public List<Rectangle> kotak
        {
            get { return _kotak; }
        }

        public List<Color> warna
        {
            get { return _warna; }
        }

        public List<Size> ukuran
        {
            get { return _ukuran; }
        }

        public List<double> jmlparticle
        {
            get { return _jmlpraticle; }
        }

        private List<Bitmap> _bitmaporang;
        private List<Histogram> _hist;
        private List<Rectangle> _kotak;
        private List<Color> _warna;
        private List<Size> _ukuran;
        private List<double> _jmlpraticle;
    }
}
