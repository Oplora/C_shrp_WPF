using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_semestr_5
{

    public struct VMTime
    {
        public int counts;
        public double WML_EP;
        public double VML_HA;


        public VMTime(int Counts, double[] Args, function F)
        {
            counts = Counts;
            double[] value_EP = new double[counts];
            double[] value_HA = new double[counts];
            Stopwatch timer = new Stopwatch();

            timer.Start();
            F(counts, Args, value_HA, 'H');
            timer.Stop();
            VML_HA = timer.Elapsed.TotalMilliseconds;
            timer.Reset();

            timer.Start();
            F(counts, Args, value_EP, 'E');
            timer.Stop();
            WML_EP = timer.Elapsed.TotalMilliseconds;
            timer.Reset();
        }

        public override string ToString()
        {
            return counts.ToString() + " " + WML_EP.ToString() + " " + VML_HA.ToString() + "\n";
        }
    }
}
