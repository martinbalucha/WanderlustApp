using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WanderlustPersistence.Entity;
using WanderlustInfrastructure.Repository;
using WanderlustService.Service.Common;
using WanderlustService.Service.QueryObject.Common;
using WanderlustService.DataTransferObject.Filter;
using WanderlustInfrastructure.Query;

namespace WanderlustService.Service.Entities.Countries
{
    /// <summary>
    /// An implementation of <see cref="ICountryService"/>
    /// </summary>
    public class CountryService : EntityServiceBase<Country>, ICountryService
    {
        /// <summary>
        /// Query object used for filtering countries
        /// </summary>
        private readonly QueryObjectBase<Country, CountryFilterDto, IQuery<Country>> queryObject;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="repository">A repository for CRUD operations on countries</param>
        public CountryService(IRepository<Country> repository, QueryObjectBase<Country, CountryFilterDto, IQuery<Country>> queryObject,
                              ILogger<CountryService> logger) : base(repository, logger)
        {
            this.queryObject = queryObject;
        }

        public async Task<QueryResult<Country>> FilterAsync(CountryFilterDto filter)
        {
            return await queryObject.ExecuteQueryAsync(filter);
        }
    }
}
