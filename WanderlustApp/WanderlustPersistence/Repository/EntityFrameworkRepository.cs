using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WanderlustInfrastructure.Entity;
using WanderlustInfrastructure.Repository;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Infrastructure.UnitOfWork;

namespace WanderlustPersistence.Repository
{
    /// <summary>
    /// An implementation of the interface <see cref="IRepository{T}"/>
    /// </summary>
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// A context for creating units of work
        /// </summary>
        private readonly IUnitOfWorkContext unitOfWorkContext;

        /// <summary>
        /// Database context
        /// </summary>
        private DbContext Context
        {
            get { return ((EntityFrameworkUnitOfWork)unitOfWorkContext.GetUnitOfWork()).Context; }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWorkContext">Database access context</param>
        public EntityFrameworkRepository(IUnitOfWorkContext unitOfWorkContext)
        {
            this.unitOfWorkContext = unitOfWorkContext;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<Guid> CreateAsync(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            await Context.Set<TEntity>().AddAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            TEntity entity = await FindAsync(id);
            Context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<TEntity> FindAsync(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }
    }
}
