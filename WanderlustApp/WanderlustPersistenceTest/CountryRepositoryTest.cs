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
    public class CountryRepositoryTest : PersistenceTestBase
    {       
        private Guid SlovakiaGuid { get; set; } = Guid.NewGuid();

        private Guid CzechRepublicGuid { get; set; } = Guid.NewGuid();

        public CountryRepositoryTest() : base()
        {
            Seed();
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

        [Fact]
        public async Task CreateCountry_Valid()
        {
            IRepository<Country> repository = Container.Resolve<IRepository<Country>>();
            Country country = new Country
            {
                Name = "Austria",
                Code = "AT",
                Description = "Once a mighty empire"
            };

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            await repository.CreateAsync(country);
            await unitOfWork.CommitAsync();
            Assert.True(country.Id != null);
        }

        [Fact]
        public async Task FindCountry_Existing()
        {
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
        public async Task FindCountry_NotExisting()
        {
            Guid guidOfNonExistingCountry = Guid.NewGuid();

            IRepository<Country> repository = Container.Resolve<IRepository<Country>>();

            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            Country retrievedCountry = await repository.FindAsync(guidOfNonExistingCountry);
            await unitOfWork.CommitAsync();
            Assert.Null(retrievedCountry);
        }

        [Fact]
        public async Task UpdateCountry_Existing()
        {
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
            IRepository<Country> repository = Container.Resolve<IRepository<Country>>();
            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();
            await repository.DeleteAsync(CzechRepublicGuid);
            await unitOfWork.CommitAsync();
        }
    }
}
