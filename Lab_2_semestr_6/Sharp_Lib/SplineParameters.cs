using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Sharp_Lib
{
    public class SplineParameters : IDataErrorInfo
    {
        public int N { get; set; }
        public double D_L { get; set; }
        public double D_R { get; set; }
        public bool Is_Err { get; set; }

        public SplineParameters()
        {
            N = 10;
        }

        public SplineParameters(double a, double b)
        {
            N = 10;
            D_L = a; 
            D_R = b;
        }

        public bool SetErr()
        {
            return Is_Err = (N <= 2);
        }

        public string this[string columnName]
        {
            get
            {
                string err = "";
                switch (columnName)
                {
                    case "N":
                        if (N <= 2)
                            err = "Число точек должно быть больше 2";
                        break;

                    default:
                        err = "";
                        break;
                }
                return err;
            }
        }
        public string Error => throw new NotImplementedException();
    }
}
