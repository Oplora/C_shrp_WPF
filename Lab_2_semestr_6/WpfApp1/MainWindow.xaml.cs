using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using Sharp_Lib;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        int Num = 0;
        public ViewData Vd { get; set; }=new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Vd.Clear();
            Num = 0;
        }

        private void MeasuredData_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !Vd.Data.Md.SetErr() && !Vd.Data.Sp.SetErr();
        }

        private void MeasuredData_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Vd.Data.Md.Func = Vd.SpfList.selectedFunc.Func;
            Vd.MdSetGrid();
            Vd.Chart.AddPlot(Vd.Data.Md.Grid, Vd.Data.Md.Measured, 2, "Points" + Num.ToString());
        }

        private void Splines_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !Vd.Data.Md.SetErr() && !Vd.Data.Sp.SetErr();
        }

        private void Splines_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            double a = 1;
            double[] Int = new double[1];
            double[] r = Vd.Spline(ref a, ref Int);
            double[] res = new double[Vd.Data.Sp.N];
            for (int i = 0; i < res.Length; i++)
                res[i] = r[3 * i];
            double[] grid = new double[Vd.Data.Sp.N];
            for (int i = 0; i < Vd.Data.Sp.N; i++)
                grid[i] = Vd.Data.Md.Start + i * (Vd.Data.Md.End - Vd.Data.Md.Start) / (Vd.Data.Sp.N - 1);

            Vd.Chart.AddPlot(grid, res, 1, "Splain " + Num.ToString());
            Num++;
        }
    }

    public static class Cmd
    {
        public static readonly RoutedUICommand MeasuredData = new
            (
                "MeasuredData",
                "MeasuredData",
                typeof(Cmd),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.D1, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Splines = new
        (
            "Splines",
            "Splines",
            typeof(Cmd),
            new InputGestureCollection()
            {
                    new KeyGesture(Key.D2, ModifierKeys.Control)
            }
        );
    }

    class DoubleToStr : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double val = (double)value;
                return $"{val:0.0}";
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "DoubleToStr: ERROR";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string val = value as string;
                return double.Parse(val);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return "DoubleToStr: ERROR";
            }
        }
    }
}
