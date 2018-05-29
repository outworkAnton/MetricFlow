using Autofac;
using AutoMapper;
using BusinessLogic.Contract;

namespace BusinessLogic.DI
{
    public class BusinessLogicAutofacModule : Module
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
            builder.Register(c => c.Resolve<MapperConfiguration>()
                   .CreateMapper(c.Resolve))
                   .As<IMapper>()
                   .InstancePerLifetimeScope();
        }
    }
}