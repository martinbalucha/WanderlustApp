using Autofac;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WanderlustInfrastructure.Repository;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Entity;
using WanderlustPersistence.Infrastructure;
using WanderlustPersistence.Infrastructure.UnitOfWork;
using WanderlustPersistence.Repository;
using Xunit;

namespace WanderlustPersistenceTest
{
    /// <summary>
    /// Contains tests for repository
    /// </summary>
    public class CountryRepositoryTest
    {
        /// <summary>
        /// Context of in memory database
        /// </summary>
        private WanderlustContext InMemoryContext { get; set; }

        /// <summary>
        /// Dependency injection container
        /// </summary>
        private IContainer Container { get; set; }

        /// <summary>
        /// Unit of work context
        /// </summary>
        private IUnitOfWorkContext UnitOfWorkContext { get; set; }

        private Guid SlovakiaGuid { get; set; } = Guid.NewGuid();

        private Guid CzechRepublicGuid { get; set; } = Guid.NewGuid();

        public CountryRepositoryTest()
        {
            var inMemoryOptions = new DbContextOptionsBuilder<WanderlustContext>().UseInMemoryDatabase(databaseName: "Test").Options;
            InMemoryContext = new WanderlustContext(inMemoryOptions);
            SetupContainer();
            UnitOfWorkContext = Container.Resolve<IUnitOfWorkContext>();
        }

        /// <summary>
        /// Inserts initial data
        /// </summary>
        private void Seed()
        {            
            var context = Container.Resolve<DbContext>();

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
                Description = "A slightly larger, neighbouring and equally beautiful country"
            };


            context.Set<Country>().Add(slovakia);
            context.Set<Country>().Add(czechRepublic);
            context.SaveChanges();

            foreach (var country in context.ChangeTracker.Entries())
            {
                country.State = EntityState.Detached;
            }
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
            Container = builder.Build();
        }

        [Fact]
        public async Task CreateCountry_Valid()
        {
            IRepository<Country> repository = Container.Resolve<IRepository<Country>>();
            Country country = new Country
            {
                Name = "Slovakia",
                Code = "SK",
                Description = "A small but beautiful country"
            };

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            await repository.CreateAsync(country);
            await unitOfWork.CommitAsync();
        }

        [Fact]
        public async Task FindCountry_Existing()
        {
            Seed();

            Country country = new Country
            {
                Id = SlovakiaGuid,
                Name = "Slovakia",
                Code = "SK",
                Description = "A small but beautiful country"
            };

            IRepository<Country> repository = Container.Resolve<IRepository<Country>>();

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            Country retrievedCountry = await repository.FindAsync(SlovakiaGuid);
            await unitOfWork.CommitAsync();
            Assert.Equal(country.Id, retrievedCountry.Id);
        }

        [Fact]
        public async Task UpdateCountry_Existing()
        {
            Seed();

            Country country = new Country
            {
                Id = CzechRepublicGuid,
                Name = "Czech Republic",
                Code = "CZ",
                Description = "A different description this time"
            };

            IRepository<Country> repository = Container.Resolve<IRepository<Country>>();

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            repository.Update(country);
            await unitOfWork.CommitAsync();
        }

        [Fact]
        public async Task DeleteCountry_Existing()
        {
            Seed();

            IRepository<Country> repository = Container.Resolve<IRepository<Country>>();
            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            await repository.DeleteAsync(CzechRepublicGuid);
            await unitOfWork.CommitAsync();
        }
    }
}
