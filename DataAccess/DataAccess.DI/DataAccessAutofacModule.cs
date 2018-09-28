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

            builder.RegisterType<LocationRepository>()
                .As<ILocationRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DataAccessContext>()
                .As<DataAccessContext>()
                .InstancePerLifetimeScope();
        }
    }
}