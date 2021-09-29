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

        private Guid SlovakiaGuid { get; set; }

        private Guid CzechRepublicGuid { get; set; }

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
        private async Task Seed()
        {            
            IRepository<Country> repository = Container.Resolve<IRepository<Country>>();

            Country slovakia = new Country
            {
                Name = "Slovakia",
                Code = "SK",
                Description = "A small but beautiful country"
            };

            Country czechRepublic = new Country
            {
                Name = "Czech Republic",
                Code = "CZ",
                Description = "A slightly larger, neighbouring and equally beautiful country"
            };

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            SlovakiaGuid = await repository.CreateAsync(slovakia);
            CzechRepublicGuid = await repository.CreateAsync(czechRepublic);
            await unitOfWork.CommitAsync();
        }

        /// <summary>
        /// Sets up autofac container
        /// </summary>
        private void SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(InMemoryContext).As(typeof(DbContext));
            builder.RegisterType(typeof(EntityFrameworkUnitOfWorkContext)).As(typeof(IUnitOfWorkContext)).SingleInstance();
            builder.RegisterGeneric(typeof(EntityFrameworkRepository<>)).As(typeof(IRepository<>));
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
            await Seed();

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

    }
}
