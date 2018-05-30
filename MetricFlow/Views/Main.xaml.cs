using System.Windows;
using MetricFlow.ViewModels;

namespace MetricFlow.Views
{
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            Loaded += delegate { MainViewModel.OnLoaded(); };
        }
    }
}