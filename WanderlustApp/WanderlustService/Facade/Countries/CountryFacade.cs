using AutoMapper;
using System.Threading.Tasks;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Entity;
using WanderlustService.DataTransferObject.Entities.Country;
using WanderlustService.Service.Entities.Countries;
using WanderlustService.Service.Entities.Users;

namespace WanderlustService.Facade.Countries
{
    /// <summary>
    /// An implementation of <see cref="ICountryFacade"/>
    /// </summary>
    public class CountryFacade : FacadeBase, ICountryFacade
    {
        /// <summary>
        /// A service class for countries
        /// </summary>
        private readonly ICountryService countryService;

        /// <summary>
        /// A service class for users
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="countryService">Service class for countries</param>
        /// <param name="userService">Service class for users</param>
        /// <param name="unitOfWorkContext">Unit of work context</param>
        /// <param name="mapper">Mapper</param>
        public CountryFacade(ICountryService countryService, IUserService userService, IUnitOfWorkContext unitOfWorkContext, IMapper mapper)
            : base(unitOfWorkContext, mapper)
        {
            this.countryService = countryService;
            this.userService = userService;
        }

        public async Task CreateAsync(CountryCreateDto countryDto)
        {
            Country country = mapper.Map<Country>(countryDto);

            IUnitOfWork unitOfWork = unitOfWorkContext.Create();
            await countryService.CreateAsync(country);
            await unitOfWork.CommitAsync();
        }

        public async Task SaveUserVisitAsync(CountrySaveVisitDto countryDto)
        {
            IUnitOfWork unitOfWork = unitOfWorkContext.Create();
            var visitedCountry = await countryService.FindAsync(countryDto.Id);
            var visitingUser = await userService.FindByUsernameAsync(countryDto.Username);

            visitedCountry.VisitedByUsers.Add(visitingUser);
            countryService.Update(visitedCountry);
            await unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(CountryUpdateDto countryDto)
        {
            IUnitOfWork unitOfWork = unitOfWorkContext.Create();
            var storedCountry = await countryService.FindAsync(countryDto.Id);
            mapper.Map(countryDto, storedCountry);
            countryService.Update(storedCountry);
            await unitOfWork.CommitAsync();
        }
    }
}
