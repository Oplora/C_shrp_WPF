using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3_semestr_5
{
    class Program
    {
        static void LKM_SIN(int n, double[] a, double[] y, char m ='a')
        {
            if (m == 's')
            {
                for (int i = 0; i < n; ++i)
                {
                    y[i] = Math.Sin(a[i]);
                }
            }
            if (m == 'c')
            {
                for (int i = 0; i < n; ++i)
                {
                    y[i] = Math.Cos(a[i]);
                }
            }


        }


        static void Main(string[] args)
        {

            /*
            double[] x = new double[100];
            for (int i =0; i < x.Length; ++i)
            {
                x[i] = i;
            }
            double[] y = new double[100];
            int n = 100;
            mkl_sin(n, x, y, 'H');
            for (int i = 0; i < x.Length; ++i)
            {
                Console.WriteLine(y[i]);
            }
            */
            VMBenchmark data = new VMBenchmark();
            data.Make(0, 1, 10000, LKM_SIN);
            data.Make(0, Math.PI, 1000000, LKM_SIN);
            data.Make(-Math.PI, Math.PI, 10000, LKM_SIN);
            data.Make(-1, 0, 100, LKM_SIN);
            data.Make(-1, 1, 10, LKM_SIN);
            Console.WriteLine(data.ToString());
            data.Save("test.txt");
            Console.ReadLine();
        }
    }
}
