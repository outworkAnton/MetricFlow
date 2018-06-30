using Autofac;
using BusinessLogic;
using BusinessLogic.Contract;

namespace MetricFlow.WebApi.DI
{
    public class ApiAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LocationService>()
                   .As<ILocationService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<FormulaService>()
                   .As<IFormulaService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<MetricService>()
                   .As<IMetricService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<RevisionService>()
                   .As<IRevisionService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<ServiceFlowService>()
                   .As<IServiceFlowService>()
                   .InstancePerLifetimeScope();
            builder.RegisterType<StatisticService>()
                   .As<IStatisticService>()
                   .InstancePerLifetimeScope();

        }
    }
}