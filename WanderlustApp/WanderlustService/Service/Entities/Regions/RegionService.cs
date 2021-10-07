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
        private readonly QueryObjectBase<RegionComponent, RegionComponentFilterDto, IQuery<RegionComponent>> queryObject;


        public Task CreateAsync(RegionComponent entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RegionComponent> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(RegionComponent entity)
        {
            throw new NotImplementedException();
        }
    }
}
