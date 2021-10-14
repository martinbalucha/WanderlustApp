using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Repository;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Entity;
using WanderlustPersistence.Infrastructure;
using WanderlustPersistence.Infrastructure.Query;
using WanderlustPersistence.Infrastructure.UnitOfWork;
using WanderlustPersistence.Repository;

namespace WanderlustPersistenceTest
{
    public abstract class PersistenceTestBase : IDisposable
    {
        protected Guid SlovakiaGuid { get; set; } = Guid.NewGuid();

        protected Guid CzechRepublicGuid { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Context of in-memory database
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
            string databaseName = Guid.NewGuid().ToString();
            var inMemoryOptions = new DbContextOptionsBuilder<WanderlustContext>().UseInMemoryDatabase(databaseName).Options;
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

        protected IList<Country> InitializeCountries()
        {
            Country slovakia = new Country
            {
                Id = SlovakiaGuid,
                Name = "Slovakia",
                Code = "SK",
                Description = "A small but beautiful country"
            };

            Country czechRepublic = new Country
            {
                Id = CzechRepublicGuid,
                Name = "Czech Republic",
                Code = "CZ",
                Description = "A small but beautiful country"
            };

            return new List<Country> { slovakia, czechRepublic };
        }

        protected void SeedCountries()
        {
            var context = Container.Resolve<DbContext>();
            var countries = InitializeCountries();

            context.Set<Country>().AddRange(countries);
            context.SaveChanges();

            foreach (var country in context.ChangeTracker.Entries())
            {
                country.State = EntityState.Detached;
            }
        }

        public void Dispose()
        {
            InMemoryContext.Database.EnsureDeleted();
        }
    }
}
