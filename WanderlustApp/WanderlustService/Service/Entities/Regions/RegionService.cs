using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WanderlustInfrastructure.Query;
using WanderlustPersistence.Entity;
using WanderlustService.DataTransferObject.Filter;
using WanderlustService.Service.QueryObject.Common;

namespace WanderlustService.Service.Entities.Regions
{
    /// <summary>
    /// An implementation of the <see cref="IRegionService"/>
    /// </summary>
    public class RegionService : IRegionService
    {
        /// <summary>
        /// A query object used for filtering users
        /// </summary>
        private readonly QueryObjectBase<Region, RegionComponentFilterDto, IQuery<Region>> queryObject;


        public Task CreateAsync(Region entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Region> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Region entity)
        {
            throw new NotImplementedException();
        }
    }
}
