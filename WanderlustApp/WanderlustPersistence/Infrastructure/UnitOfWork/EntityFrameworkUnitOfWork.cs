using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustResource.Backend;

namespace WanderlustPersistence.Infrastructure.UnitOfWork
{
    /// <summary>
    /// An implementation of the <see cref="UnitOfWorkBase"/>
    /// </summary>
    public class EntityFrameworkUnitOfWork : UnitOfWorkBase
    {
        /// <summary>
        /// A database context
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">DB context</param>
        public EntityFrameworkUnitOfWork(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException(string.Format(Exceptions.WLE006, nameof(context)));
            }
            Context = context;
        }

        public override void Dispose()
        {
            Context.Dispose();
        }

        protected async override Task CommitCoreAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
