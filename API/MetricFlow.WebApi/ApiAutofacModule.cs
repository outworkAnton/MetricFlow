
using Autofac;
using BusinessLogic.Contract.Interfaces;
using BusinessLogic.Contract.Models;

namespace MetricFlow.WebApi
{
    public class ApiAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Location>()
                .As<ILocation>()
                .InstancePerLifetimeScope();
            builder.RegisterType<Formula>()
                .As<IFormula>()
                .InstancePerLifetimeScope();
            builder.RegisterType<Metric>()
                .As<IMetric>()
                .InstancePerLifetimeScope();
            builder.RegisterType<Service>()
                .As<IService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<Statistic>()
                .As<IStatistic>()
                .InstancePerLifetimeScope();
        }
    }
}