using System;
using System.Collections.Generic;

using Autofac;

using AutoMapper;

using BusinessLogic.Contract;
using BusinessLogic.Contract.Interfaces;

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
            builder.RegisterType<ServiceFlowService>()
                .As<IServiceFlowService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StatisticService>()
                .As<IStatisticService>()
                .InstancePerLifetimeScope();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();
            builder.Register(c => c.Resolve<MapperConfiguration>()
                    .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}