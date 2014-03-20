using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.VFW;
using AForge.Video.FFMPEG;
using AForge.Imaging.Filters;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.GPU;


namespace vidplay
{
    public partial class Form1 : Form
    {
        public int bin = 8; public int flag2 = 1;
        public int flag = 1;
        public int mandek = 0;
        public int radius = 30;
        public Bitmap orang1 = null;
        public Bitmap orang2 = null;
        public Histogram histori1 = null;
        public Histogram histori2 = null;
        public Histogram histori = null;
        public double rasiox=0.75;
        public double rasioy=0.825;
        public int Nparticle = 40;
        public double threshold = 0.80;
        public Rectangle rectori1,rectori;
        public Rectangle rectori2;
        public FileVideoSource vidsor = null;
        public Image<Bgr, Byte> imageemgu = null;
        public int flagth = 1;
        public int lebar, panjang;
        public string selectedFileName = null;
        Processor _imgProc = new Processor();
        
        // variable hasil
        public List<long> waktuproses = new List<long>();
        public List<Rectangle> letak = new List<Rectangle>();
        public List<int> jumlahparticle = new List<int>();

        objek manusia = null;
        public Rectangle rectemp;
        public int jmlorang;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "I:\\kaziz\\ta\\video";
            dlg.Filter = "video files (*.avi; *.MP4; *.mov)|*.avi; *.MP4; *.mov|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mandek = 0;
                string selectedFileName = dlg.FileName;
                textBox11.Text = selectedFileName;
            }
        }

        #region processing frame
        void fvs_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Stopwatch waktuframe;
            waktuframe = Stopwatch.StartNew();

            #region multiobject
            if (flag2 == 2)
            {
                Bitmap frame = eventArgs.Frame;
                Bitmap bm = (Bitmap)frame.Clone();
                imageemgu = new Emgu.CV.Image<Bgr, Byte>(bm);
                long processingTime,waktu;

                if (flag == 1)
                {
                    lebar = bm.Width; panjang = bm.Height;
                    System.Drawing.Rectangle[] results = pejalan.Find(imageemgu, out processingTime); jmlorang = results.GetLength(0);
                    textBox10.Text = Convert.ToString(results.GetLength(0));
                    if (results.GetLength(0) == 0)
                    {
                        imageBox1.Image = imageemgu;
                    }

                    textBox9.Text = Convert.ToString(processingTime);
                    if (jmlorang >= 2)
                    {
                        objek manusia1 = new objek(results.GetLength(0));
                        for (int i = 0; i < jmlorang; i++)
                        {
                            rectori1.Width = Convert.ToInt16(results[i].Width * 0.5); rectori1.Height = Convert.ToInt16(results[i].Height * 0.75);
                            rectori1.X = Convert.ToInt16(results[i].X + 0.25 * results[i].Width); rectori1.Y = Convert.ToInt16(results[i].Y + 0.4 * results[i].Height);
                            Size tempsize = new Size(Convert.ToInt16(rectori1.Width * rasiox), Convert.ToInt16(rectori1.Height * rasioy));
                            Bitmap orang1 = kelas.kroping.cropori(bm, results[i], tempsize, out waktu);
                            histori1 = CreateHistogram((Bitmap)orang1);

                            manusia1.kotak.Insert(i, rectori1);
                            manusia1.bitmaporang.Insert(i, orang1);
                            manusia1.hist.Insert(i, histori1);
                            manusia1.ukuran.Insert(i, tempsize);
                            manusia1.jmlparticle.Insert(i, threshold);
                            pictureBox1.Image = manusia1.bitmaporang[i];
                            flag++;
                        }
                        manusia = manusia1;
                    }
                }
                if (flag >= 2)
                {
                    for (int i = 0; i < jmlorang; i++)
                    {
                        kemungkinan(bm, manusia.kotak, manusia.ukuran, manusia.hist, Nparticle, manusia.warna);
                    }
                }
            }
            #endregion
            #region one obejct
            else if (flag2 == 1)
            {
                Bitmap frame = eventArgs.Frame;
                Bitmap bm = (Bitmap)frame.Clone();
                imageemgu = new Image<Bgr, byte>(bm);
                long processingTime,waktu;

                if (flag == 1)
                {
                    lebar = bm.Width; panjang = bm.Height;
                    System.Drawing.Rectangle[] results = pejalan.Find(imageemgu, out processingTime); jmlorang = results.GetLength(0);
                    textBox10.Text = Convert.ToString(results.GetLength(0));
                    if (results.GetLength(0) == 0)
                    {
                        imageBox1.Image = imageemgu;
                    }

                    textBox9.Text = Convert.ToString(processingTime);
                    if (jmlorang != 0)
                    {
                        objek manusia1 = new objek(results.GetLength(0));
                        for (int i = 0; i < jmlorang; i++)
                        {
                            rectori1.Width = Convert.ToInt16(results[i].Width * 0.5); rectori1.Height = Convert.ToInt16(results[i].Height * 0.75);
                            rectori1.X = Convert.ToInt16(results[i].X + 0.25 * results[i].Width); rectori1.Y = Convert.ToInt16(results[i].Y + 0.4 * results[i].Height);
                            Size tempsize = new Size(Convert.ToInt16(rectori1.Width * rasiox), Convert.ToInt16(rectori1.Height * rasioy));
                            Bitmap orang1 = kelas.kroping.cropori(bm, results[i], tempsize, out waktu);
                            histori1 = CreateHistogram((Bitmap)orang1);

                            manusia1.kotak.Insert(i, rectori1);
                            manusia1.bitmaporang.Insert(i, orang1);
                            manusia1.hist.Insert(i, histori1);
                            manusia1.ukuran.Insert(i, tempsize);
                            manusia1.jmlparticle.Insert(i, threshold);
                            pictureBox1.Image = manusia1.bitmaporang[i];
                            flag++;
                        }
                        manusia = manusia1;
                    }
                }
                if (flag >= 2)
                {
                    kemungkinan(bm, manusia.kotak, manusia.ukuran, manusia.hist, Nparticle, manusia.warna);
                }
            }
            #endregion
            waktuframe.Stop();
            long wk = waktuframe.ElapsedMilliseconds;
            textBox7.Text = Convert.ToString(wk);
            this.waktuproses.Add(wk);
            this.letak.Add(manusia.kotak[0]);
        }
#endregion

        private Histogram CreateHistogram(Bitmap img)
        {
            return _imgProc.Create1DHistogram(img, bin, bin, bin);
        }

        private float Bhattacharyya(Histogram hist1, Histogram hist2,double x,double y,Point xy,int rad,Size size)
        {
            double coeff = 0;
            float t = 0;
            for (int i = 0; i < hist1.Data.Length; i++)
            {
                coeff += Math.Sqrt((double)hist1.Data[i] * (double)hist2.Data[i]);
                t += hist1.Data[i];
            }
            return (float)(coeff);
        }

        #region kemungkinan
        void kemungkinan(Bitmap image, List<Rectangle> rect, List<Size> size, List<Histogram> hist, int jumlah, List<Color> warna)
        {
            Stopwatch w; int rad;
            w = Stopwatch.StartNew();
            Bitmap temp = image;
            Image<Bgr,Byte> tempemgu=null;
            List<float[]> hasilakhir;
            hasilakhir = new List<float[]>(jmlorang);

            for (int j = 0; j < jmlorang; j++)
            {
                double xx = manusia.jmlparticle[j];
                if (xx <= 0.8)
                {
                    rad = manusia.ukuran[j].Width;
                }
                else { rad = Convert.ToInt16(manusia.ukuran[j].Width * 0.5); }

                double centerx = rect[j].X - (rect[j].Width * .5); double centery = rect[j].Y - (rect[j].Height * .5);
                double x = 0; double y = 0;
                int jml = 0;
                float[] hasil = new float[jumlah];
                Point[] particleF = kelas.randompoint.buattitik(jumlah, Convert.ToInt16(rect[j].X + 0.5 * rect[j].Width - 0.5 * size[j].Width), Convert.ToInt16(rect[j].Y + 0.5 * rect[j].Height - 0.5 * size[j].Height), rad, size[j],lebar,panjang);
                for (int i = 0; i < particleF.Length; i++)
                {
                    hasil[i] = Bhattacharyya(hist[j], cropclone(image, particleF[i], size[j]), centerx, centery, particleF[i], radius, size[j]);
                    if (hasil[i] > xx)
                    {
                        temp.SetPixel(Convert.ToInt16(particleF[i].X + 0.5 * size[j].Width), Convert.ToInt16(particleF[i].Y + 0.5 * size[j].Height), warna[j]);
                        x += particleF[i].X + 0.5 * size[j].Width;
                        y += particleF[i].Y + 0.5 * size[j].Height;
                        jml++;
                    }
                    hasilakhir.Insert(j, hasil);
                }

                this.jumlahparticle.Add(jml);

                if (flagth == 1)
                {
                    manusia.jmlparticle[j] = (8.9 + (Math.Log(Convert.ToDouble(jml + 1) / Nparticle)) / 7 * 2.75)/10;
                    textBox1.Text = Convert.ToString(manusia.jmlparticle[j]);
                }

                textBox8.Text = Convert.ToString(jml);
                if (jml > 3)
                {
                    Rectangle tempx = new Rectangle(Convert.ToInt16(x / jml - 0.5 * rect[j].Width), Convert.ToInt16(y / jml - 0.5 * rect[j].Height), rect[j].Width, rect[j].Height);
                    manusia.kotak[j] = tempx;
                }
                tempemgu=new Image<Bgr,byte>(temp);
                tempemgu.Draw(manusia.kotak[j], new Bgr(manusia.warna[j]), 1);
                
                if (j < jmlorang-1)
                {
                    temp=tempemgu.ToBitmap();
                }


            }
            w.Stop();
            long det = w.ElapsedMilliseconds;

            imageBox1.Image = tempemgu;
        }

        #endregion
        private Histogram cropclone(Bitmap image, Point poin, Size size)
        {
            Rectangle rect = new Rectangle(poin, size);
            Bitmap hasil = image.Clone(rect, image.PixelFormat);
            Histogram hslhist = CreateHistogram(hasil);
            return hslhist;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flag = 1;
            orang1 = null;
            orang2 = null;
            histori = null;
            histori1 = null;
            histori2 = null;
            rectori = new Rectangle();
            rectori1 = new Rectangle();
            rectori2 = new Rectangle();
            pictureBox1.Image = null;
            imageBox1.Image = null;
        }

        #region textbok input

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Nparticle = Int16.Parse(textBox2.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            threshold = double.Parse(textBox1.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            bin = Int16.Parse(textBox3.Text);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            rasiox = Double.Parse(textBox4.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            rasioy = Double.Parse(textBox5.Text);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            radius = int.Parse(textBox6.Text);
        }
        
#endregion
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                flag2 = 1;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                flag2 = 2;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (vidsor != null)
            {
                vidsor.SignalToStop();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                flagth = 1;
            }
            else
            {
                flagth = 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            trackinghog hog = new trackinghog();
            hog.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox11.Text!=null)
            {
                vidsor = new FileVideoSource(textBox11.Text);
                vidsor.NewFrame += new AForge.Video.NewFrameEventHandler(fvs_NewFrame);
                vidsor.Start();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='D:\\asisten riset\\data kaziz\\database_ta\\data1.xlsx';Extended Properties=Excel 8.0;");
            System.Data.OleDb.OleDbDataAdapter dataadapter = null;
            conn.Open();
            try
            {
                for (int i = 0; i < waktuproses.Count; i++)
                {
                    int j = i + 1;
                    string sql = "Insert into [Sheet1$](id,waktu,partikel,x,y,w,h) Values('" + j + "','"+jumlahparticle[i]+"','" + waktuproses[i] + "','" + letak[i].X + "','" + letak[i].Y + "','" + letak[i].Width + "','" + letak[i].Height + "')";
                    dataadapter = new System.Data.OleDb.OleDbDataAdapter(sql, conn);
                    dataadapter.SelectCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                conn.Dispose();
                MessageBox.Show(ex.ToString());
            }


            dataadapter.Dispose();
            conn.Close();
            conn.Dispose();
            MessageBox.Show("data succesfully");
        }

    }
}
