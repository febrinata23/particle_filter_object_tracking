using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace vidplay
{
    class particle
    {
        private static double gauss(double mean, double stdDev)
        {
            Random rand=new Random();
            double u1 = rand.NextDouble();
            double u2 = rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = mean + Math.Sqrt(stdDev) * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }
        public void particleawal(double step_noise,double theta_noise,int numParticles,double step_size,double theta,double x,double y)
        {
            for (int i = 0; i < numParticles; i++)
            {
                double nr = step_size + gauss(0, step_noise);
                double ntheta = theta + gauss(0, theta_noise);
                double new_x = x + nr * Math.Cos(ntheta);
                new_x = new_x > 1.0 ? new_x - 1.0 : new_x;

                double new_y = y + nr * Math.Sin(ntheta);
                new_y = new_y > 1.0 ? new_y - 1.0 : new_y;

            }
            double[] bobot = new double[numParticles];
            for (int i = 0; i < numParticles; i++)
            {
                bobot[i] = 1 / numParticles;
            }
        }
    }
}
