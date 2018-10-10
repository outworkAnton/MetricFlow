using Autofac;

using DataAccess.Contract.Repositories;
using DataAccess.Repositories;

namespace DataAccess.DI
{
    public class DataAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LocationRepository>()
                .As<ILocationRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<MetricRepository>()
                .As<IMetricRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ServiceRepository>()
                .As<IServiceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DataAccessContext>()
                .As<DataAccessContext>()
                .InstancePerLifetimeScope();
        }
    }
}