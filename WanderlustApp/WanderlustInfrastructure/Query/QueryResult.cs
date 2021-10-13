using System.Collections.Generic;
using WanderlustInfrastructure.Entity;

namespace WanderlustInfrastructure.Query
{
    /// <summary>
    /// Contains results of queries
    /// </summary>
    public class QueryResult<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// A number of entities satisfying the query conditions
        /// </summary>
        public long TotalItemCount { get; private set; }

        /// <summary>
        /// A number of the requested page
        /// </summary>
        public int? RequestedPageNumber { get; set; }

        /// <summary>
        /// Size of the page
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// A collection of filtered entities
        /// </summary>
        public IEnumerable<TEntity> Items { get; }


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="items">A collection of filtered items</param>
        /// <param name="totalItemCount">The number of filtered items</param>
        /// <param name="pageSize">Size of one page</param>
        /// <param name="requestedPageNumber">Number of the requested page</param>
        public QueryResult(IEnumerable<TEntity> items, long totalItemCount, int pageSize = 20, int? requestedPageNumber = null)
        {
            TotalItemCount = totalItemCount;
            RequestedPageNumber = requestedPageNumber;
            PageSize = pageSize;
            Items = items;
        }
    }
}
