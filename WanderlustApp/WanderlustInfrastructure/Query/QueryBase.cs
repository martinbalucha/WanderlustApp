using System;
using System.Linq;
using System.Threading.Tasks;
using WanderlustInfrastructure.Entity;
using WanderlustInfrastructure.Query.Predicates;
using WanderlustInfrastructure.UnitOfWork;
using WanderlustResource.Backend;

namespace WanderlustInfrastructure.Query
{
    /// <summary>
    /// A base class for the query. Implements <see cref="IQuery{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">A type of persistent entity</typeparam>
    public abstract class QueryBase<TEntity> : IQuery<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// The default size of a single page
        /// </summary>
        private const int DefaultPageSize = 20;

        /// <summary>
        /// Sorting property
        /// </summary>
        private string sortProperty;

        /// <summary>
        /// A page size. Defaultly set to the value of 20
        /// </summary>
        public int PageSize { get; private set; } = DefaultPageSize;

        /// <summary>
        /// The number of the wanted page
        /// </summary>
        public int? PageNumber { get; private set; }

        /// <summary>
        /// Sorting property
        /// </summary>
        public string SortingProperty
        {
            get { return sortProperty; }
            set
            {
                var properties = typeof(TEntity).GetProperties().Select(prop => prop.Name);
                var matchedName = properties.FirstOrDefault(name => name.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
                sortProperty = matchedName;
            }
        }

        /// <summary>
        /// Determines whether the results will be sorted ascendingly or not
        /// </summary>
        public bool SortAscendingly { get; protected set; }

        /// <summary>
        /// Selection predicate
        /// </summary>
        public IPredicate Predicate { get; protected set; }

        /// <summary>
        /// Context for creating units of work
        /// </summary>
        protected readonly IUnitOfWorkContext unitOfWorkContext;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWorkContext">A context for creating units of work</param>
        public QueryBase(IUnitOfWorkContext unitOfWorkContext)
        {
            this.unitOfWorkContext = unitOfWorkContext;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public abstract Task<QueryResult<TEntity>> ExecuteAsync();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IQuery<TEntity> Page(int pageNumber, int pageSize = 20)
        {
            if (PageNumber < 1)
            {
                throw new ArgumentException(Exceptions.WLE001);
            }
            if (pageSize < 1)
            {
                throw new ArgumentException(Exceptions.WLE002);
            }
            PageNumber = pageNumber;
            PageSize = pageSize;
            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IQuery<TEntity> SortBy(string sortingProperty, bool ascendingOrder = true)
        {
            if (string.IsNullOrEmpty(sortingProperty))
            {
                throw new ArgumentException($"{nameof(sortingProperty)} must be defined!");
            }            
            SortingProperty = sortingProperty;
            SortAscendingly = ascendingOrder;
            return this;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IQuery<TEntity> Where(IPredicate predicate)
        {
            Predicate = predicate ?? throw new ArgumentException(Exceptions.WLE003);
            return this;
        }
    }
}
