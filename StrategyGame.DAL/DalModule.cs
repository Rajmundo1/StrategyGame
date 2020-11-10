using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.DAL
{
    public class DalModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ApplicationDbContext).Assembly;
            builder.RegisterAssemblyTypes(assembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
