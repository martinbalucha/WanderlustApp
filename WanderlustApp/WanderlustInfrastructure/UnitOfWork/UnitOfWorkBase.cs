using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WanderlustInfrastructure.UnitOfWork
{
    /// <summary>
    /// A base class for the unit of work. Implements <see cref="IUnitOfWork"/>
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        /// <summary>
        /// A list of actions that will be performed after commit.
        /// </summary>
        private readonly IList<Action> afterCommitActions = new List<Action>();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async Task CommitAsync()
        {
            await CommitCoreAsync();
            foreach (Action action in afterCommitActions)
            {
                action();
            }
            afterCommitActions.Clear();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void RegisterAction(Action action)
        {
            afterCommitActions.Add(action);
        }

        /// <summary>
        /// Performs the real commit work.
        /// </summary>
        protected abstract Task CommitCoreAsync();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public abstract void Dispose();
    }
}
