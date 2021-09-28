using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using WanderlustInfrastructure.Entity;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Query.Predicates;
using WanderlustInfrastructure.Query.Predicates.Operators;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustPersistence.Infrastructure.UnitOfWork;
using WanderlustResource.Backend;

namespace WanderlustPersistence.Infrastructure.Query
{
    /// <summary>
    /// An implementation of the <see cref="IQuery{TEntity}"/> and <see cref="QueryBase{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">A type of a persistent entity</typeparam>
    public class EntityQuery<TEntity> : QueryBase<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// The name of the lambda parameter
        /// </summary>
        private const string LamdaParameterName = "param";

        /// <summary>
        /// Parameter expression
        /// </summary>
        private readonly ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), LamdaParameterName);

        /// <summary>
        /// Gets database context
        /// </summary>
        private DbContext Context
        {
            get { return ((EntityFrameworkUnitOfWork)unitOfWorkContext.GetUnitOfWork()).Context; }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWorkContext">Unit of work context</param>
        public EntityQuery(IUnitOfWorkContext unitOfWorkContext) : base(unitOfWorkContext)
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public async override Task<QueryResult<TEntity>> ExecuteAsync()
        {
            IQueryable<TEntity> queryable = Context.Set<TEntity>();

            if (string.IsNullOrWhiteSpace(SortingProperty) && PageNumber.HasValue)
            {
                SortingProperty = nameof(EntityBase.Id);
                SortAscendingly = true;
            }
            if (SortingProperty != null)
            {
                queryable = UseSortCriteria(queryable);
            }
            if (Predicate != null)
            {
                queryable = UseFilterCriteria(queryable);
            }
            var itemsCount = queryable.Count();
            if (PageNumber.HasValue)
            {
                queryable = queryable.Skip(PageSize * (PageNumber.Value - 1)).Take(PageSize);
            }
            var items = await queryable.ToListAsync();
            return new QueryResult<TEntity>(items, itemsCount, PageSize, PageNumber);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        private IQueryable<TEntity> UseSortCriteria(IQueryable<TEntity> queryable)
        {
            var selectedProeprty = typeof(TEntity).GetProperty(SortingProperty);
            var param = Expression.Parameter(typeof(TEntity), "i");
            var expr = Expression.Lambda(Expression.Property(param, selectedProeprty), param);
            return (IQueryable<TEntity>)typeof(EntityQuery<TEntity>)
                .GetMethod(nameof(UseSortCriteriaCore), BindingFlags.Instance | BindingFlags.NonPublic)
                .MakeGenericMethod(selectedProeprty.PropertyType)
                .Invoke(this, new object[] { expr, queryable });
        }

        /// <summary>
        /// Applies sorting criteria
        /// </summary>
        /// <typeparam name="TKey">Key</typeparam>
        /// <param name="sortExpression">Sorting expression</param>
        /// <param name="queryable">A queryable collection</param>
        /// <returns>Sorted queryable collection</returns>
        private IQueryable<TEntity> UseSortCriteriaCore<TKey>(Expression<Func<TEntity, TKey>> sortExpression, IQueryable<TEntity> queryable)
        {
            return SortAscendingly ? queryable.OrderBy(sortExpression) : queryable.OrderByDescending(sortExpression);
        }

        private IQueryable<TEntity> UseFilterCriteria(IQueryable<TEntity> queryable)
        {
            var bodyExpression = Predicate is CompositePredicate composite ? CombineBinaryExpressions(composite) : BuildBinaryExpression(Predicate as ElementaryPredicate);
            var lambdaExpression = Expression.Lambda<Func<TEntity, bool>>(bodyExpression, parameterExpression);
            Debug.WriteLine(lambdaExpression.ToString());
            return queryable.Where(lambdaExpression);
        }

        /// <summary>
        /// Conbines binary expressions
        /// </summary>
        /// <param name="compositePredicate">A single composite predicate</param>
        /// <returns>Predicate transformed into an expression</returns>
        private Expression CombineBinaryExpressions(CompositePredicate compositePredicate)
        {
            if (compositePredicate.Predicates.Count == 0)
            {
                throw new InvalidOperationException(Exceptions.WLE004);
            }
            var expression = compositePredicate.Predicates.First() is CompositePredicate composite
                ? CombineBinaryExpressions(composite)
                : BuildBinaryExpression(compositePredicate.Predicates.First());
            for (var i = 1; i < compositePredicate.Predicates.Count; i++)
            {
                if (compositePredicate.Predicates[i] is CompositePredicate predicate)
                {
                    expression = compositePredicate.LogicalOperator == LogicalOperator.OR ?
                        Expression.OrElse(expression, CombineBinaryExpressions(predicate)) :
                        Expression.AndAlso(expression, CombineBinaryExpressions(predicate));
                }
                else
                {
                    expression = compositePredicate.LogicalOperator== LogicalOperator.OR ?
                        Expression.OrElse(expression, BuildBinaryExpression(compositePredicate.Predicates[i])) :
                        Expression.AndAlso(expression, BuildBinaryExpression(compositePredicate.Predicates[i]));
                }
            }
            return expression;
        }

        /// <summary>
        /// Builds new binary expression from the predicate
        /// </summary>
        /// <param name="predicate">A predicate containing selection condition</param>
        /// <returns>A binary expression derived from the predicate</returns>
        private Expression BuildBinaryExpression(IPredicate predicate)
        {
            ElementaryPredicate elementaryPredicate = predicate as ElementaryPredicate;
            if (elementaryPredicate == null)
            {
                throw new ArgumentException(Exceptions.WLE005);
            }
            return elementaryPredicate.GetExpression(parameterExpression);
        }
    }
}
