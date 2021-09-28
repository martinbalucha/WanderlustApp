using System.Collections.Generic;
using WanderlustInfrastructure.Query.Predicates;
using WanderlustPersistence.Entity;
using WanderlustInfrastructure.Query;
using WanderlustService.DataTransferObject.Filter;
using WanderlustService.Service.QueryObject.Common;
using WanderlustInfrastructure.Query.Predicates.Operators;

namespace WanderlustService.Service.Query.QueryObject
{
    /// <summary>
    /// Query object used for filtering users
    /// </summary>
    public class UserQueryObject : QueryObjectBase<User, UserFilterDto, IQuery<User>>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="query">Query for filtering useres</param>
        public UserQueryObject(IQuery<User> query) : base(query)
        {
        }

        protected override IList<IPredicate> BuildPredicates(UserFilterDto filter)
        {
            IList<IPredicate> predicates = new List<IPredicate>();
            AddPredicateIfDefined(FilterByUsername(filter), predicates);
            AddPredicateIfDefined(FilterByEmail(filter), predicates);
            return predicates;
        }

        /// <summary>
        /// Builds predicate for filtering by username
        /// </summary>
        /// <param name="filter">Filter used for building predicates</param>
        /// <returns>A filled predicate</returns>
        private IPredicate FilterByUsername(UserFilterDto filter)
        {
            if (!string.IsNullOrEmpty(filter.Username))
            {
                return new ElementaryPredicate(nameof(User.Username), ValueComparingOperator.Equal, filter.Username);
            }
            return null;
        }

        /// <summary>
        /// Builds predicate for filtering by email
        /// </summary>
        /// <param name="filter">Filter used for building predicates</param>
        /// <returns>A filled predicate</returns>
        private IPredicate FilterByEmail(UserFilterDto filter)
        {
            if (!string.IsNullOrEmpty(filter.Email))
            {
                return new ElementaryPredicate(nameof(User.Email), ValueComparingOperator.Equal, filter.Email);
            }
            return null;
        }
    }
}
