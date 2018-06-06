using Autofac;
using DataAccess.Contract;
using DataAccess.Contract.Interfaces;

namespace DataAccess.DI
{
    public class DataAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseRevisionRepository>()
                   .As<IDatabaseRevisionRepository>()
                   .InstancePerLifetimeScope();

            builder.Register(e => new DataAccessContext())
                   .As<DataAccessContext>()
                   .InstancePerLifetimeScope();
        }
    }
}