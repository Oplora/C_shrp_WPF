using System;
using System.Runtime.InteropServices;

namespace Custom_MKL_Console // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        [DllImport("Custom_MKL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int mkl_sin(int n, double[] a, double[] y, char m);

        static void Main(string[] args)
        {
            double[] a = new double[10];
            double[] b = new double[10];
            Console.WriteLine(mkl_sin(0,a,b,'0'));
        }
    }
}