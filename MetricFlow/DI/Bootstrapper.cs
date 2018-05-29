using System.Windows;
using Autofac;
using BusinessLogic;
using BusinessLogic.Contract;
using BusinessLogic.DI;
using DataAccess;
using DataAccess.Contract;
using DataAccess.DI;
using MetricFlow.ViewModels;
using MetricFlow.Views;
using Prism.Autofac;

namespace MetricFlow.DI
{
    public class Bootstrapper : AutofacBootstrapper
    {
        /// <summary>Creates the shell or main window of the application.</summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        /// <see cref="T:Prism.Bootstrapper" /> will attach the default <see cref="T:Prism.Regions.IRegionManager" /> of
        /// the application in its <see cref="F:Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
        /// in order to be able to add regions by using the <see cref="F:Prism.Regions.RegionManager.RegionNameProperty" />
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Main>();
        }

        /// <summary>Initializes the shell.</summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window) Shell;
            Application.Current.MainWindow.Show();
        }

        protected override ContainerBuilder CreateContainerBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<BusinessLogicAutofacModule>();
            builder.RegisterModule<DataAccessAutofacModule>();
            //builder.Register(c => new MainViewModel(Container.Resolve<RevisionService>()));
            ConfigureContainerBuilder(builder);
            return builder;
        }
    }
}