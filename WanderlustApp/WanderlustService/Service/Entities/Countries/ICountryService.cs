using System.Threading.Tasks;
using WanderlustInfrastructure.Query;
using WanderlustPersistence.Entity;
using WanderlustService.DataTransferObject.Filter;
using WanderlustService.Service.Common;

namespace WanderlustService.Service.Entities.Countries
{
    /// <summary>
    /// A contract for a service manipulating with <see cref="Country"/>
    /// </summary>
    public interface ICountryService : IEntityService<Country>
    {
        /// <summary>
        /// Filters country according to the selected criteria
        /// </summary>
        /// <param name="filter">Filter containg selection requirements</param>
        /// <returns>Query result containing collection of countries satifying the criteria</returns>
        Task<QueryResult<Country>> FilterAsync(CountryFilterDto filter);
    }
}
