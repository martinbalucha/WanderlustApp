using System.Threading.Tasks;
using WanderlustInfrastructure.Entity;
using WanderlustInfrastructure.Query.Predicates;

namespace WanderlustInfrastructure.Query
{
    /// <summary>
    /// An interface for the database query
    /// </summary>
    /// <typeparam name="TEntity">A persitent entity</typeparam>
    public interface IQuery<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// Sets filtering condition
        /// </summary>
        /// <param name="predicate">Predicate that contains a condition</param>
        /// <returns>Filled query</returns>
        IQuery<TEntity> Where(IPredicate predicate);

        /// <summary>
        /// Sets sorting property
        /// </summary>
        /// <param name="sortingProperty">The name of the sorting property</param>
        /// <param name="ascendingOrder">Determines whether the results will be ordered ascendingly or not</param>
        /// <returns>Filled query</returns>
        IQuery<TEntity> SortBy(string sortingProperty, bool ascendingOrder = true);

        /// <summary>
        /// Sets a page that it to be fetched from the database
        /// </summary>
        /// <param name="pageNumber">The number of the page</param>
        /// <param name="pageSize">The size of the page</param>
        /// <returns>Filled query</returns>
        IQuery<TEntity> Page(int pageNumber, int pageSize = 20);

        /// <summary>
        /// Asynchronously executes a query
        /// </summary>
        /// <returns>A query result containing entities fetched from the database</returns>
        Task<QueryResult<TEntity>> ExecuteAsync();
    }
}
