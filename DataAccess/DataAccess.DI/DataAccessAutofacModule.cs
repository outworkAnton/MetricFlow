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
            builder.RegisterType<DatabaseRevisionRepository>()
                .As<IDatabaseRevisionRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DataAccessContext>()
                .As<DataAccessContext>()
                .InstancePerLifetimeScope();
        }
    }
}