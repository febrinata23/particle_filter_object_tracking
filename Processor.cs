using System;
using System.Drawing;
using System.Collections.Specialized;


namespace vidplay
{
    public unsafe class Processor
    {
        public Processor()
        {
        }

        public Bitmap Crop(Bitmap bmp, Rectangle roi)
        {
            Bitmap croppedImage = new Bitmap(roi.Width, roi.Height, bmp.PixelFormat);

            UnsafeBitmap fastBitmap = new UnsafeBitmap(bmp);
            UnsafeBitmap fastBitmapCrp = new UnsafeBitmap(croppedImage);
            fastBitmap.LockBitmap();
            fastBitmapCrp.LockBitmap();
            BGRA* pPixel;
            BGRA* pPixelCrp;
            int yCrp = 0;
            int xCrp = 0;

            for (int y = roi.Y; y < roi.Y + roi.Height; y++)
            {
                xCrp = 0;
                for (int x = roi.X; x < roi.X + roi.Width; x++)
                {
                    pPixel = fastBitmap[x, y];
                    pPixelCrp = fastBitmapCrp[xCrp, yCrp];

                    pPixelCrp->red = pPixel->red;
                    pPixelCrp->green = pPixel->green;
                    pPixelCrp->blue = pPixel->blue;

                    //increment x coord
                    xCrp++;
                }
                yCrp++;
            }

            fastBitmap.UnlockBitmap();
            fastBitmapCrp.UnlockBitmap();

            return croppedImage;
        }
             
        public Histogram Create1DHistogram(Bitmap bmp, int numBinsCh1, int numBinsCh2, int numBinsCh3)
        {
            Histogram hist = new Histogram(numBinsCh1 * numBinsCh2 * numBinsCh3);
            float total = 0;
            int idx = 0;

            UnsafeBitmap fastBitmap = new UnsafeBitmap(bmp);
            fastBitmap.LockBitmap();
            Point size = fastBitmap.Size;
            BGRA* pPixel;

            for (int y = 0; y < size.Y; y++)
            {
                pPixel = fastBitmap[0, y];
                for (int x = 0; x < size.X; x++)
                {
                    //get the bin index for the current pixel colour
                    idx = GetSingleBinIndex(numBinsCh1, numBinsCh2, numBinsCh3, pPixel);
                    hist.Data[idx] += 1;
                    total += 1;

                    //increment the pointer
                    pPixel++;
                }
            }

            fastBitmap.UnlockBitmap();

            //normalise
            if (total > 0)
                hist.Normalise(total);

            return hist;
        }

        private int GetSingleBinIndex(int binCount1, int binCount2, int binCount3, BGRA* pixel)
        {
            int idx = 0;
            
            //find the index                
            int i1 = GetBinIndex(binCount1, (float)pixel->red, 255);
            int i2 = GetBinIndex(binCount2, (float)pixel->green, 255);
            int i3 = GetBinIndex(binCount3, (float)pixel->blue, 255);
            idx = i1 + i2 * binCount1 + i3 * binCount1 * binCount2;

            return idx;
        }

        private int GetBinIndex(int binCount, float colourValue, float maxValue)
        {
            int idx = (int)(colourValue * (float)binCount / maxValue);
            if (idx >= binCount)
                idx = binCount - 1;

            return idx;
        }

        #region "IProcessor Interface"

        public Histogram Create1DHistogram(Bitmap bitmap, int numBinsCh1, int numBinsCh2, int numBinsCh3, Rectangle targetRoi, Rectangle searchRoi)
        {
            //ignore searchRoi
            Bitmap bmp = (Bitmap)bitmap.Clone(targetRoi, bitmap.PixelFormat);
            Histogram hist = new Histogram(numBinsCh1*numBinsCh2*numBinsCh3);
            float total = 0;
            int idx = 0;

            UnsafeBitmap fastBitmap = new UnsafeBitmap(bmp);
            fastBitmap.LockBitmap();
            Point size = fastBitmap.Size;
            BGRA* pPixel;

            for (int y = 0; y < size.Y; y++)
            {
                pPixel = fastBitmap[0, y];
                for (int x = 0; x < size.X; x++)
                {
                    //get the bin index for the current pixel colour
                    idx = GetSingleBinIndex(numBinsCh1, numBinsCh2, numBinsCh3, pPixel);
                    hist.Data[idx] += 1;
                    total += 1;

                    //increment the pointer
                    pPixel++;
                }
            }

            fastBitmap.UnlockBitmap();

            //normalise
            if (total > 0)
                hist.Normalise(total);

            return hist;
        }
        #endregion

    }

}