using System;
using System.Collections.Generic;
using System.Text;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Query.Predicates;
using WanderlustInfrastructure.Query.Predicates.Operators;
using WanderlustPersistence.Entity;
using WanderlustService.DataTransferObject.Filter;
using WanderlustService.Service.QueryObject.Common;

namespace WanderlustService.Service.Query.QueryObject
{
    /// <summary>
    /// A query object for countries
    /// </summary>
    public class CountryQueryObject : QueryObjectBase<Country, CountryFilterDto, IQuery<Country>>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="query">A query executor</param>
        public CountryQueryObject(IQuery<Country> query) : base(query)
        {
        }

        protected override IList<IPredicate> BuildPredicates(CountryFilterDto filter)
        {
            IList<IPredicate> predicates = new List<IPredicate>();
            AddPredicateIfDefined(FilterByName(filter), predicates);
            AddPredicateIfDefined(FilterByVisit(filter), predicates);

            return predicates;
        }

        /// <summary>
        /// Filters countries by their names
        /// </summary>
        /// <param name="filter">Filter used for building predicates</param>
        /// <returns>A filled predicate</returns>
        private IPredicate FilterByName(CountryFilterDto filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                return new ElementaryPredicate(nameof(Country.Name), ValueComparingOperator.StringContains, filter.Name);
            }
            return null;
        }

        /// <summary>
        /// Filters countries by user visits
        /// </summary>
        /// <param name="filter">Filter used for buidling predicates</param>
        /// <returns>A filled predicate</returns>
        private IPredicate FilterByVisit(CountryFilterDto filter)
        {
            if (!string.IsNullOrEmpty(filter.User) && filter.Visited.HasValue)
            {
                var valueComparer = filter.Visited.Value ? ValueComparingOperator.In : ValueComparingOperator.NotIn;
                return new ElementaryPredicate(nameof(Country.VisitedByUsers), valueComparer, filter.User);
            }
            return null;
        }
    }
}
