using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp_Lib;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    public class List : INotifyPropertyChanged
    {
        public Spf Func { get; set; }
        public string F { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class SpfList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public List selectedFunc;
        public ObservableCollection<List> Funcs { get; set; }

        public SpfList()
        {
            Funcs = new ObservableCollection<List>
            {
                new List { Func = Spf.Linear, F = "Linear" },
                new List { Func = Spf.Cubic, F = "Cubic" },
                new List { Func = Spf.Random, F = "Random" }
            };
            selectedFunc = Funcs[0];
        }
        public List SelectedFunc
        {
            get { return selectedFunc; }
            set
            {
                selectedFunc = value;
                OnPropertyChanged(nameof(SelectedFunc));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

