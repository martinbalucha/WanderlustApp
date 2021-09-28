using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WanderlustInfrastructure.Entity;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Query.Predicates;
using WanderlustService.DataTransferObject.Filter.Common;

namespace WanderlustService.Service.QueryObject.Common
{
    /// <summary>
    /// An abstract class for query objects
    /// </summary>
    public abstract class QueryObjectBase<TEntity, TFilter, TQuery>
        where TEntity : EntityBase
        where TFilter : FilterDtoBase
        where TQuery: IQuery<TEntity>
    {
        /// <summary>
        /// A query used for filtering
        /// </summary>
        protected readonly IQuery<TEntity> query;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="query">A query used for filtering</param>
        public QueryObjectBase(IQuery<TEntity> query)
        {
            this.query = query;
        }

        /// <summary>
        /// Applies where clause contained in the filter to the query
        /// </summary>
        /// <param name="query">A query that will be filled with criteria</param>
        /// <param name="filter">A filter carrying the filter criteria</param>
        /// <returns>Filled query</returns>
        protected IQuery<TEntity> ApplyWhereClause(IQuery<TEntity> query, TFilter filter)
        {
            var predicates = BuildPredicates(filter);
            
            if (predicates.Count == 0)
            {
                return query;
            }
            if (predicates.Count == 1)
            {
                return query.Where(predicates.First());
            }

            var compositePredicate = new CompositePredicate(predicates);
            return query.Where(compositePredicate);
        }

        /// <summary>
        /// Builds predicates for the selection
        /// </summary>
        /// <param name="filter">Filter used for building predicates</param>
        /// <returns>A list of entities containing selection predicates</returns>
        protected abstract IList<IPredicate> BuildPredicates(TFilter filter);

        /// <summary>
        /// Asynchronously executes the query
        /// </summary>
        /// <param name="filter">Filter containing selection criteria</param>
        /// <returns>Query result that contains entities that satisfy the selection criteria</returns>
        public virtual async Task<QueryResult<TEntity>> ExecuteQueryAsync(TFilter filter)
        {
            var queryWithCondition = ApplyWhereClause(query, filter);
            if (!string.IsNullOrWhiteSpace(filter.SortProperty))
            {
                queryWithCondition.SortBy(filter.SortProperty, filter.SortAscendingly);
            }
            if (filter.RequestedPageNumber.HasValue)
            {
                queryWithCondition.Page(filter.RequestedPageNumber.Value, filter.PageSize);
            }

            var queryResult = await query.ExecuteAsync();
            return queryResult;
        }

        /// <summary>
        /// If the predicate has value, the method adds it to the list of predicates
        /// </summary>
        /// <param name="predicate">The current predicate</param>
        /// <param name="predicates">A list of predicates</param>
        protected void AddPredicateIfDefined(IPredicate predicate, IList<IPredicate> predicates)
        {
            if (predicate != null)
            {
                predicates.Add(predicate);
            }
        }
    }
}
