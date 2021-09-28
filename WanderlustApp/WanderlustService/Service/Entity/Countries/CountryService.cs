using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WanderlustPersistence.Entity;
using WanderlustInfrastructure.Repository;
using WanderlustService.Service.Common;

namespace WanderlustService.Service.Entities.Countries
{
    /// <summary>
    /// An implementation of <see cref="ICountryService"/>
    /// </summary>
    public class CountryService : EntityServiceBase<Country>, ICountryService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="repository">A repository for CRUD operations on countries</param>
        public CountryService(IRepository<Country> repository, ILogger<CountryService> logger) : base(repository, logger)
        {
        }
    }
}
