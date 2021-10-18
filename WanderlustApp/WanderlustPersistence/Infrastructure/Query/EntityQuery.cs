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
            get { return ((EntityFrameworkUnitOfWork) unitOfWorkContext.GetUnitOfWork()).Context; }
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

        private IQueryable<TEntity> UseSortCriteriaCore<TKey>(Expression<Func<TEntity, TKey>> sortExpression, IQueryable<TEntity> queryable)
        {
            return SortAscendingly ? queryable.OrderBy(sortExpression) : queryable.OrderByDescending(sortExpression);
        }

        private IQueryable<TEntity> UseFilterCriteria(IQueryable<TEntity> queryable)
        {
            var bodyExpression = Predicate.BuildExpression(parameterExpression);
            var lambdaExpression = Expression.Lambda<Func<TEntity, bool>>(bodyExpression, parameterExpression);
            Debug.WriteLine(lambdaExpression.ToString());
            return queryable.Where(lambdaExpression);
        }
    }
}
