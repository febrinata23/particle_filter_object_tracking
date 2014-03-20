using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.VFW;
using AForge.Video.FFMPEG;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.GPU;

namespace vidplay
{
    class imageproc
    {
        public static Image<Bgr,Byte> colorhist(Image<Bgr,Byte> images)
        {
            Byte[, ,] matpix = images.Data;
        }
    }
}
