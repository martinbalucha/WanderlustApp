using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Repository;
using WanderlustPersistence.Entity;
using WanderlustService.DataTransferObject.Filter;
using WanderlustService.Service.Common;
using WanderlustService.Service.QueryObject.Common;

namespace WanderlustService.Service.Entities.Regions
{
    /// <summary>
    /// An implementation of the <see cref="IRegionService"/>
    /// </summary>
    public class RegionService : EntityServiceBase<Region>, IRegionService
    {
        /// <summary>
        /// A query object used for filtering users
        /// </summary>
        private readonly QueryObjectBase<Region, RegionFilterDto, IQuery<Region>> queryObject;

        public RegionService(IRepository<Region> repository, ILogger<RegionService> logger,
                             QueryObjectBase<Region, RegionFilterDto, IQuery<Region>> queryObject) : base(repository, logger)
        {
            this.queryObject = queryObject;
        }

        public async Task<QueryResult<Region>> FilterAsync(RegionFilterDto filter)
        {
            return await queryObject.ExecuteQueryAsync(filter);
        }
    }
}
