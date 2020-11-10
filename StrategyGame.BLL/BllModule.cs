using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL
{
    public class BllModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(MapperProfiles).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(type => type.Name.EndsWith("AppService"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
