using Microsoft.EntityFrameworkCore;
using System;
using WanderlustInfrastructure.UnitOfWork;

namespace WanderlustPersistence.Infrastructure.UnitOfWork
{
    /// <summary>
    /// An implementation of <see cref="UnitOfWorkContextBase"/>
    /// </summary>
    public class EntityFrameworkUnitOfWorkContext : UnitOfWorkContextBase
    {
        /// <summary>
        /// A factory method for creating database context
        /// </summary>
        private readonly DbContext context;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dbContextFactory">A factory method for creating database context</param>
        public EntityFrameworkUnitOfWorkContext(DbContext context)
        {
            this.context = context;
        }

        public override IUnitOfWork Create()
        {
            unitOfWorkLocalInstance.Value = new EntityFrameworkUnitOfWork(context);
            return unitOfWorkLocalInstance.Value;
        }
    }
}
