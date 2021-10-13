using Autofac;
using Microsoft.EntityFrameworkCore;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Repository;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Infrastructure;
using WanderlustPersistence.Infrastructure.Query;
using WanderlustPersistence.Infrastructure.UnitOfWork;
using WanderlustPersistence.Repository;

namespace WanderlustPersistenceTest
{
    public abstract class PersistenceTestBase
    {
        /// <summary>
        /// Context of in memory database
        /// </summary>
        protected WanderlustContext InMemoryContext { get; set; }

        /// <summary>
        /// Dependency injection container
        /// </summary>
        protected IContainer Container { get; set; }

        /// <summary>
        /// Unit of work context
        /// </summary>
        protected IUnitOfWorkContext UnitOfWorkContext { get; set; }

        public PersistenceTestBase()
        {
            var inMemoryOptions = new DbContextOptionsBuilder<WanderlustContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            InMemoryContext = new WanderlustContext(inMemoryOptions);
            SetupContainer();
            UnitOfWorkContext = Container.Resolve<IUnitOfWorkContext>();
        }

        /// <summary>
        /// Sets up autofac container
        /// </summary>
        private void SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(InMemoryContext).As(typeof(DbContext));
            builder.RegisterType(typeof(EntityFrameworkUnitOfWorkContext)).As(typeof(IUnitOfWorkContext)).SingleInstance();
            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EntityQuery<>)).As(typeof(IQuery<>)).InstancePerLifetimeScope();
            Container = builder.Build();
        }
    }
}
