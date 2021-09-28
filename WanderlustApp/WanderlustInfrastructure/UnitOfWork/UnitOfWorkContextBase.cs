using System;
using System.Threading;
using WanderlustResource.Backend;

namespace WanderlustInfrastructure.UnitOfWork
{
    /// <summary>
    /// Abstract implementation of the <see cref="IUnitOfWorkContext"/>
    /// </summary>
    public abstract class UnitOfWorkContextBase : IUnitOfWorkContext
    {
        /// <summary>
        /// A local instance of unit of work
        /// </summary>
        protected readonly AsyncLocal<IUnitOfWork> unitOfWorkLocalInstance = new AsyncLocal<IUnitOfWork>();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public abstract IUnitOfWork Create();

        public void Dispose()
        {
            unitOfWorkLocalInstance.Value?.Dispose();
            unitOfWorkLocalInstance.Value = null;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IUnitOfWork GetUnitOfWork()
        {
            if (unitOfWorkLocalInstance != null)
            {
                return unitOfWorkLocalInstance.Value;
            }
            throw new InvalidOperationException(Exceptions.WLE010);
        }
    }
}
