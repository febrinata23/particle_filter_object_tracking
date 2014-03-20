using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.Util;
using Emgu.Util;

namespace vidplay
{
    public partial class trackinghog : Form
    {
        public FileVideoSource vidsor = null;

        public trackinghog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "D:\\ta";
            dlg.Filter = "video files (*.avi)|*.avi|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                string selectedFileName = dlg.FileName;
                textBox3.Text = selectedFileName;

            }
        }

        void fvs_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap frame = eventArgs.Frame;
            Bitmap bm = (Bitmap)frame.Clone();
            Emgu.CV.Image<Bgr,Byte> imageemgu = new Emgu.CV.Image<Bgr, Byte>(bm);

            long processingTime;
            System.Drawing.Rectangle[] results = pejalan.Find(imageemgu, out processingTime);
            textBox1.Text = Convert.ToString(processingTime);
            if (results.GetLength(0) != 0)
            {
                for (int i = 0; i < results.GetLength(0); i++)
                {
                    imageemgu.Draw(results[i], new Bgr(Color.Red), 1);
                }
            }

            textBox2.Text = Convert.ToString(results.GetLength(0));
            imageBox1.Image = imageemgu;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form1 pf = new Form1();
            pf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != null)
            {
                vidsor = new FileVideoSource(textBox3.Text);
                vidsor.NewFrame += new AForge.Video.NewFrameEventHandler(fvs_NewFrame);
                vidsor.Start();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (vidsor != null)
            {
                vidsor.SignalToStop();
            }
        }

    }
}
