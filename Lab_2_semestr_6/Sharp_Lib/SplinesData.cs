using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Sharp_Lib
{
    public class SplinesData
    {
        [DllImport("\\..\\..\\..\\..\\x64\\Debug\\Custom_MKL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Interpolate(int length, double[] points, double[] func, double[] res, double[] der, int gridlen, double[] grid, double[] limits, double[] integrals);

        public MeasuredData Md { get; set; }
        public SplineParameters Sp { get; set; }
        public double[] Nodes { get; set; }
        public double Integral { get; set; }
        public double D_L { get; set; }
        public double D_R { get; set; }
        public ObservableCollection<string> Info;
        public SplinesData(MeasuredData md, SplineParameters mp)
        {
            Md = md; 
            Sp = mp;
            Nodes = new double[3*Sp.N];
            Info = new();
            //for (int i=0; i < Sp.N; i++)
            //{
            //    Nodes[i] = Md.Start+i/(Sp.N-1)*(Md.End- Md.Start);
            //}
        }
        public SplinesData()
        {
            Md = new(); 
            Sp = new();
            Nodes = new double[3 * Sp.N];
            Info = new();
        }

        public double[] Spline(ref double a, ref double[] Int)
        {
            Nodes = new double[3 * Sp.N];
            a = Interpolate(Md.N, Md.Grid, Md.Measured, Nodes, new double[] { Sp.D_L, Sp.D_R }, Sp.N, new double[] { Md.Start, Md.End }, new double[] { Md.Int_Start, Md.Int_End }, Int);
            Integral = Int[0];
            D_L = Nodes[2];
            D_R = Nodes[3 * Sp.N-1];
            return Nodes;
        }

        public ObservableCollection<string> MeasuredData_Str
        {
            get
            {
                Md.Info = new();
                for (int i = 0; i < Md.N; i++) 
                    Md.Info.Add($"x[{i + 1}]: {Md.Grid[i]:f4}\t\ty[{i + 1}]: {Md.Measured[i]:f4}");
                return Md.Info;
            }
            set
            {
                Md.Info = value;
            }
        }

        public ObservableCollection<string> SplinesData_Str
        {
            get
            {
                Info = new();
                Info.Add($"Init derivatives: D_L={Sp.D_L:f4}");
                Info.Add($"D_R={Sp.D_R:f4}");
                for (int i = 0; i < Sp.N; i++)
                    Info.Add($"x[{i + 1}]: {(double)i/(Sp.N-1):f4}\t\ty[{i + 1}]: {Nodes[3*i]:f4}");
                Info.Add($"Calculated derivatives: Der_L={D_L:f4}");
                Info.Add($"Der_R={D_R:f4}");
                Info.Add($"Calculated Integral: I={Integral:f4}");
                return Info;
            }
            set
            {
                Info = value;
            }
        }
    }

}
