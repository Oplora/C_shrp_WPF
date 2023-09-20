using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace WpfApp1
{
    public class ChartData
    {
        public SeriesCollection Sc { get; set; }
        public Func<double, string> Form { get; set; }
        public ChartData()
        {
            Sc = new();
            Form = value => value.ToString("F2");
        }

        public void AddPlot(double[] grid, double[] measured, int mode, string title)
        {
            ChartValues<ObservablePoint> Values = new();
            for (int i = 0; i < measured.Length; i++)
            {
                Values.Add(new(grid[i], measured[i]));
            }
            if (mode == 1)
                Sc.Add(new LineSeries { Title = title, Values = Values, PointGeometry = null });
            if (mode == 2)
                Sc.Add(new ScatterSeries { Title = title, Values = Values });
        }
        public void Clear()
        {
            Sc.Clear();
        }
    }
}
