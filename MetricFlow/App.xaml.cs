using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using BusinessLogic.Contract;
using BusinessLogic.Contract.Exceptions;
using MetricFlow.DI;

namespace MetricFlow
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstapper = new Bootstrapper();
            bootstapper.Run();
        }
    }
}