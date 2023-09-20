using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_semestr_5
{
    public struct VMAccuracy
    {
        public double beginning;
        public double ending;
        public int counts;
        public double max_abs;
        public double max_HA;
        public double max_EP;
        public double max_X_axis;

        public VMAccuracy(int Counts, double Beginning, double Ending, double[] Args, function f)
        {
            beginning = (Beginning <= Ending) ? Beginning : Ending;
            ending = (Beginning <= Ending) ? Ending : Beginning;
            max_abs = 0;
            counts = Counts;
            

            int k = 0;
            double[] value_EP = new double[counts];
            //f(counts, Args, value_EP, 'E');
            f(counts, Args, value_EP, 's');
            double[] value_HA = new double[counts];
            //f(counts, Args, value_HA, 'H');
            f(counts, Args, value_EP, 'c');
            for (int i = 0; i < counts; i++)
            {
                double max = Math.Abs(value_HA[i] - value_EP[i]);
                if (max >= max_abs)
                {
                    max_abs = max;
                    k = i;
                }
            }
            max_X_axis = Args[k];
            max_HA = value_HA[k];
            max_EP = value_EP[k];

        }

        public override string ToString()
        {
            return beginning.ToString() + " " + ending.ToString() + " " + counts.ToString() + " " + max_abs.ToString() + " " + max_EP.ToString() + " " + max_HA.ToString() + " " + max_X_axis.ToString() +"\n";
        }
    }
}
