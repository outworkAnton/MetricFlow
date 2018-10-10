using Autofac;

using DataAccess.Contract.Interfaces;
using DataAccess.Contract.Repositories;
using DataAccess.Repositories;

namespace DataAccess.DI
{
    public class DataAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataAccessRepository<ILocation>>()
                .As<IDataAccessRepository<ILocation>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<DataAccessRepository<IService>>()
                .As<IDataAccessRepository<IService>>()
                .InstancePerLifetimeScope();
            builder.RegisterType<DataAccessRepository<IMetric>>()
                .As<IDataAccessRepository<IMetric>>()
                .InstancePerLifetimeScope();

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