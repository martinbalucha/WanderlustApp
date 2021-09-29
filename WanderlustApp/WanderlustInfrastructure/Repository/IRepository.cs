using System;
using System.Threading.Tasks;
using WanderlustInfrastructure.Entity;

namespace WanderlustInfrastructure.Repository
{
    /// <summary>
    /// Contract for a generic repository responsible for data access
    /// </summary>
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// Asynchronically creates a new entity
        /// </summary>
        /// <param name="entity">A new entity</param>
        /// <returns>Guid of a new entity</returns>
        Task<Guid> CreateAsync(TEntity entity);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity">An entity, which is to be updated</param>
        void Update(TEntity entity);

        /// <summary>
        /// Asynchronically finds an entity with the given ID
        /// </summary>
        /// <param name="id">ID of the wanted entity</param>
        /// <returns>Entity with the given ID. Null, if no such entity exists</returns>
        Task<TEntity> FindAsync(Guid id);

        /// <summary>
        /// Asynchronically removes an entity from the database
        /// </summary>
        /// <param name="id">ID of an entity that is to be removed</param>
        /// <returns>Task</returns>
        Task DeleteAsync(Guid id);
    }
}
