using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using WanderlustInfrastructure.Repository;

namespace WanderlustInfrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class AutofacInfrastructureConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(IRepository<>))
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
