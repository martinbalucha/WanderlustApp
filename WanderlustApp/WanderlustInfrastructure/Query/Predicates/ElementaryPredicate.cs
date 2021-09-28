using System;
using WanderlustInfrastructure.Query.Predicates.Operators;

namespace WanderlustInfrastructure.Query.Predicates
{
    /// <summary>
    /// A class representing an elementary predicate
    /// </summary>
    public class ElementaryPredicate : IPredicate
    {
        /// <summary>
        /// A name of the target property
        /// </summary>
        public string TargetProperty { get; private set; }

        /// <summary>
        /// Value comparing operator
        /// </summary>
        public ValueComparingOperator ValueComparingOperator { get; private set; }

        /// <summary>
        /// A compared value
        /// </summary>
        public object ComparedValue { get; private set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="targetProperty">A name of the target property</param>
        /// <param name="valueComparingOperator">A value comparing operator</param>
        /// <param name="comparedValue">A compared value</param>
        public ElementaryPredicate(string targetProperty, ValueComparingOperator valueComparingOperator, object comparedValue)
        {
            TargetProperty = targetProperty;
            ValueComparingOperator = valueComparingOperator;
            ComparedValue = comparedValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is ElementaryPredicate)
            {
                var otherPredicate = obj as ElementaryPredicate;
                return string.Equals(TargetProperty, otherPredicate.TargetProperty)
                    && ComparedValue.Equals(otherPredicate.ComparedValue)
                    && ValueComparingOperator.Equals(otherPredicate.ValueComparingOperator);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TargetProperty, ValueComparingOperator, ComparedValue);
        }
    }
}
