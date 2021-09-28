using Microsoft.EntityFrameworkCore;
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
        /// <param name="context">Database access context</param>
        public EntityFrameworkRepository(IUnitOfWorkContext context)
        {
            this.unitOfWorkContext = context;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task CreateAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task DeleteAsync(long id)
        {
            TEntity entity = await FindAsync(id);
            Context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task<TEntity> FindAsync(long id)
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
