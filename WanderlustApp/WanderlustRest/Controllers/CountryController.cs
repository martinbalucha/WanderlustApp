using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderlustService.DataTransferObject.Entities.Country;
using WanderlustService.Facade.Countries;

namespace WanderlustRest.Controllers
{
    /// <summary>
    /// A controller responsible for handling actions on countries
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        /// <summary>
        /// Country facade
        /// </summary>
        private readonly ICountryFacade countryFacade;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="countryFacade">Country facade</param>
        public CountryController(ICountryFacade countryFacade)
        {
            this.countryFacade = countryFacade;
        }        

        /// <summary>
        /// Creates a new country
        /// </summary>
        /// <param name="countryDto">DTO containing basic country information</param>
        /// <returns>Task</returns>
        [HttpPost("Create")]
        public async Task Create(CountryCreateDto countryDto)
        {
            await countryFacade.CreateAsync(countryDto);
        }

        /// <summary>
        /// Asynchronously updates a country
        /// </summary>
        /// <param name="countryDto">A DTO of a country that will be updated</param>
        /// <returns>Task</returns>
        [HttpPost("Update")]
        public async Task Update(CountryUpdateDto countryDto)
        {
            if (ModelState.IsValid)
            {
                await countryFacade.UpdateAsync(countryDto);
            }    
        }
    }
}
