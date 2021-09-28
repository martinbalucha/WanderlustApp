using System;

namespace WanderlustInfrastructure.UnitOfWork
{
    /// <summary>
    /// An interface for the context responsible for creating unit of work
    /// </summary>
    public interface IUnitOfWorkContext : IDisposable
    {
        /// <summary>
        /// Creates new unit of work
        /// </summary>
        /// <returns>A new unit of work</returns>
        IUnitOfWork Create();

        /// <summary>
        /// Gets the currently opened unit of work
        /// </summary>
        /// <returns>Currently opened unit of work</returns>
        IUnitOfWork GetUnitOfWork();
    }
}
