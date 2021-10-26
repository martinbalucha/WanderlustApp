using System.Collections.Generic;
using WanderlustInfrastructure.Query;
using WanderlustInfrastructure.Query.Predicates;
using WanderlustInfrastructure.Query.Predicates.Operators;
using WanderlustPersistence.Entity;
using WanderlustService.DataTransferObject.Filter;
using WanderlustService.Service.QueryObject.Common;

namespace WanderlustService.Service.Query.QueryObject
{
    /// <summary>
    /// 
    /// </summary>
    public class RegionQueryObject : QueryObjectBase<Region, RegionFilterDto, IQuery<Region>>
    {
        public RegionQueryObject(IQuery<Region> query) : base(query)
        {
        }

        protected override IList<IPredicate> BuildPredicates(RegionFilterDto filter)
        {
            IList<IPredicate> predicates = new List<IPredicate>();
            AddPredicateIfDefined(FilterByName(filter), predicates);
            AddPredicateIfDefined(FilterByVisit(filter), predicates);
            AddPredicateIfDefined(FilterByCountry(filter), predicates);

            return predicates;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private IPredicate FilterByName(RegionFilterDto filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                return new ElementaryPredicate(nameof(Region.Name), ValueComparingOperator.StringContains, filter.Name);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private IPredicate FilterByVisit(RegionFilterDto filter)
        {
            if (!string.IsNullOrEmpty(filter.User) && filter.Visited.HasValue)
            {
                var valueComparer = filter.Visited.Value ? ValueComparingOperator.In : ValueComparingOperator.NotIn;
                return new ElementaryPredicate(nameof(Region.VisitedByUsers), valueComparer, filter.User);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private IPredicate FilterByCountry(RegionFilterDto filter)
        {
            if (!string.IsNullOrEmpty(filter.Country))
            {
                return new ElementaryPredicate("Country.Name", ValueComparingOperator.Equal, filter.Country);
            }
            return null;
        }
    }
}
