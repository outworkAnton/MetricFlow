using Autofac;
using DataAccess.Contract;

namespace DataAccess.DI
{
    public class DataAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataAccessRepository>()
                   .As<IDataAccessRepository>()
                   .InstancePerLifetimeScope();

            builder.Register(e => new DataAccessContext())
                   .As<DataAccessContext>()
                   .InstancePerLifetimeScope();
        }
    }
}