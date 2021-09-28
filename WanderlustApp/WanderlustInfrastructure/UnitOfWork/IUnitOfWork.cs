using System;
using System.Threading.Tasks;

namespace WanderlustInfrastructure.UnitOfWork
{
    /// <summary>
    /// Interfaces for a unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Asynchronously commits the current transaction
        /// </summary>
        /// <returns>Task</returns>
        Task CommitAsync();

        /// <summary>
        /// Registers the callback that will be launched with the commit
        /// </summary>
        /// <param name="action">Action callback</param>
        void RegisterAction(Action action);
    }
}
