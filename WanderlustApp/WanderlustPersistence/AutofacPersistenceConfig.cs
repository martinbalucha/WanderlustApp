using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Repository;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Entity;
using WanderlustPersistence.Infrastructure;
using WanderlustPersistence.Infrastructure.Query;
using WanderlustPersistence.Infrastructure.UnitOfWork;
using WanderlustPersistence.Repository;

namespace WanderlustPersistence
{
    /// <summary>
    /// Registers types from Persistence to a IoC container
    /// </summary>
    public class AutofacPersistenceConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(WanderlustContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType<EntityFrameworkUnitOfWorkContext>().As<IUnitOfWorkContext>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>)).As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(EntityQuery<>)).As(typeof(IQuery<>)).InstancePerDependency();
        }
    }
}
