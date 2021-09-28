using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WanderlustService.DataTransferObject.Entities.Country;

namespace WanderlustService.Facade.Countries
{
    /// <summary>
    /// An interface for operations based on manipulating with countries
    /// </summary>
    public interface ICountryFacade
    {
        /// <summary>
        /// Creates a new country
        /// </summary>
        /// <param name="countryDto">A new country</param>
        /// <returns>Task</returns>
        Task CreateAsync(CountryCreateDto countryDto);

        /// <summary>
        /// Updates a country
        /// </summary>
        /// <param name="countryDto">DTO of a country that will be updated</param>
        Task UpdateAsync(CountryUpdateDto countryDto);

        /// <summary>
        /// Asynchronously marks the country as visited
        /// </summary>
        /// <param name="countryDto">DTO for a country visit</param>
        /// <returns>Task</returns>
        Task SaveUserVisitAsync(CountrySaveVisitDto countryDto);
    }
}
