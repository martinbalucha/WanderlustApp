using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WanderlustInfrastructure.Entity;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Repository;

namespace WanderlustService.Service.Common
{
    /// <summary>
    /// Abstract class representing a service used for manipulation with entities
    /// </summary>
    public abstract class EntityServiceBase<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// A repository used for CRUD operations on an entity
        /// </summary>
        protected readonly IRepository<TEntity> repository;

        /// <summary>
        /// A logger
        /// </summary>
        protected readonly ILogger logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="repository">A repository used for CRUD operations on an entity</param>
        /// <param name="logger">Logger</param>
        public EntityServiceBase(IRepository<TEntity> repository, ILogger logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        /// <summary>
        /// Asynchronically creates a new entity
        /// </summary>
        /// <param name="sight">A new entity that will be created</param>
        /// <returns>Task</returns>
        public virtual async Task CreateAsync(TEntity entity)
        {
            await repository.CreateAsync(entity);
        }

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity">An entity that will be updated</param>
        public virtual void Update(TEntity entity)
        {
            repository.Update(entity);
        }

        /// <summary>
        /// Asynchronically finds the entity by the given ID
        /// </summary>
        /// <param name="id">ID of the wanted entity</param>
        /// <returns>An entity with the given ID. Null, if no such entity exists</returns>
        public virtual async Task<TEntity> FindAsync(Guid id)
        {
            return await repository.FindAsync(id);
        }

        /// <summary>
        /// Asynchronously deletes an entity with the given ID
        /// </summary>
        /// <param name="id">ID of an entity that is to be deleted</param>
        /// <returns>Task</returns>
        public virtual async Task DeleteAsync(Guid id)
        {
            await repository.DeleteAsync(id);
        }
    }
}
