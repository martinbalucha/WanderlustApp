using Microsoft.Extensions.Logging;
using System;
using WanderlustInfrastructure.Repository;
using WanderlustPersistence.Entity;
using WanderlustService.Service.Common;

namespace WanderlustService.Service.Entities.Sights
{
    /// <summary>
    /// An implementation of the interface <see cref="ISightService"/>
    /// </summary>
    public class SightService : EntityServiceBase<Sight>, ISightService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="repository">A repository used for CRUD operations on a <see cref="Sight"/></param>
        /// <param name="logger">Logger</param>
        public SightService(IRepository<Sight> repository, ILogger<SightService> logger) : base(repository, logger)
        {
        }
    }
}
