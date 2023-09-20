using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_3_semestr_5
{
    public delegate void function(int n, double[] a, double[] y, char m);
    public class VMBenchmark
    {
        [DllImport("C:/Users/oplor/source/repos/Lab_3_semestr_5/Build/x64/Debug/mkl_functions.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mkl_sin(int n, double[] a, double[] y, char m);


        [DllImport("mkl_functions.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mkl_cos(int n, double[] a, double[] y, char m);

        [DllImport("mkl_functions.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void mkl_sincos(int n, double[] a, double[] y, char m);

        public List<VMTime> time_results = new List<VMTime>();
        public List<VMAccuracy> accuracy_results = new List<VMAccuracy>();

        public void Make(double Beginning, double Ending, int Counts, function f)
        {
            try
            {
                double step = (Ending - Beginning) / (Counts - 1);
                double[] args = new double[Counts];
                for (int i = 0; i < Counts; ++i)
                {
                    args[i] = Beginning + i * step;
                }

                VMTime time_Result_sin = new VMTime(Counts, args, f);
                //VMTime time_Result_cos = new VMTime(Counts, args, mkl_cos);
                //VMTime time_Result_sincos = new VMTime(Counts, args, mkl_sincos);

                VMAccuracy acc_Result_sin = new VMAccuracy(Counts, Beginning, Ending, args, f);
                //VMAccuracy acc_Result_cos = new VMAccuracy(Counts, Beginning, Ending, args, mkl_cos);
                //VMAccuracy acc_Result_sincos = new VMAccuracy(Counts, Beginning, Ending, args, mkl_sincos);

                time_results.Add(time_Result_sin);
                //time_results.Add(time_Result_cos);
                //time_results.Add(time_Result_sincos);

                accuracy_results.Add(acc_Result_sin);
                //accuracy_results.Add(acc_Result_cos);
                //accuracy_results.Add(acc_Result_sincos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

            public override string ToString()
        {
            string info = "";
            foreach (var elem in time_results)
            {
                info += elem.ToString() + "\n";
            }
            foreach (var elem in accuracy_results)
            {
                info += elem.ToString();
            }

            return info;
        }
        
        public bool Save(string filename)
        {
            Stream file = null;
            try
            {
                using (file = new FileStream(filename, FileMode.Append))
                {
                    foreach(var item in time_results)
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(VMTime));
                        xmlSerializer.Serialize(file, item);
                    }
                    foreach (var item in accuracy_results)
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(VMAccuracy));
                        xmlSerializer.Serialize(file, item);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (file != null) file.Close();
            }
        }
       
    }
}

