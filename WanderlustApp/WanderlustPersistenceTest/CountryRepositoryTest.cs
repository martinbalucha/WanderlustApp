using Autofac;
using System;
using System.Threading.Tasks;
using WanderlustInfrastructure.Repository;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Entity;
using Xunit;

namespace WanderlustPersistenceTest
{
    /// <summary>
    /// Contains tests for repository
    /// </summary>
    public class CountryRepositoryTest : PersistenceTestBase
    {      
        public CountryRepositoryTest() : base()
        {
            SeedCountries();
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

        [Fact]
        public async Task DeleteCountry_NonExisting()
        {
            IRepository<Country> repository = Container.Resolve<IRepository<Country>>();

            Guid notStoredGuid = Guid.NewGuid();
            IUnitOfWork unitOfWork = UnitOfWorkContext.Create();

            Func<Task> action = async () => await repository.DeleteAsync(notStoredGuid);
            await Assert.ThrowsAsync<ArgumentNullException>(action);
            await unitOfWork.CommitAsync();
        }
    }
}
