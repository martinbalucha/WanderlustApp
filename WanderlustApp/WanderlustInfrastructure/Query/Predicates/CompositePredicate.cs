using System;
using System.Collections.Generic;
using System.Linq;
using WanderlustInfrastructure.Query.Predicates.Operators;

namespace WanderlustInfrastructure.Query.Predicates
{
    /// <summary>
    /// A composite predicate
    /// </summary>
    public class CompositePredicate : IPredicate
    {
        /// <summary>
        /// A list of subpredicates
        /// </summary>
        public IList<IPredicate> Predicates { get; private set; }

        /// <summary>
        /// A logical operator
        /// </summary>
        public LogicalOperator LogicalOperator { get; private set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="predicates">A list of subpredicates</param>
        /// <param name="logicalOperator">A logical operator</param>
        public CompositePredicate(IList<IPredicate> predicates, LogicalOperator logicalOperator = LogicalOperator.AND)
        {
            Predicates = predicates;
            LogicalOperator = logicalOperator;
        }

        protected bool Equals(CompositePredicate other)
        {
            return new HashSet<IPredicate>(Predicates.Where(predicate => predicate is ElementaryPredicate))
                       .SetEquals(new HashSet<IPredicate>(other.Predicates.Where(predicate => predicate is ElementaryPredicate)))
                   && LogicalOperator == other.LogicalOperator;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == GetType() && Equals((CompositePredicate)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Predicates, LogicalOperator);
        }
    }
}
