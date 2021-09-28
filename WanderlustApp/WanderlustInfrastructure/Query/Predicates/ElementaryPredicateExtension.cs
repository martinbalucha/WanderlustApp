using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WanderlustResource.Backend;
using WanderlustInfrastructure.Query.Predicates.Operators;
using WanderlustInfrastructure.Entity;

namespace WanderlustInfrastructure.Query.Predicates
{
    /// <summary>
    /// An extension class for <see cref="ElementaryPredicate"/>
    /// Copied from: TODO
    /// </summary>
    public static class ElementaryPredicateExtension
    {
        /// <summary>
        /// Dictionary containing value comparing operators and their expression equivalents
        /// </summary>
        private static readonly IDictionary<ValueComparingOperator, Func<MemberExpression, ConstantExpression, Expression>> Expressions =
            new Dictionary<ValueComparingOperator, Func<MemberExpression, ConstantExpression, Expression>>
        {
            {ValueComparingOperator.Equal, Expression.Equal },
            {ValueComparingOperator.NotEqual, Expression.NotEqual },
            {ValueComparingOperator.GreaterThan, Expression.GreaterThan },
            {ValueComparingOperator.GreaterThanOrEqual, Expression.GreaterThanOrEqual },
            {ValueComparingOperator.LessThan, Expression.LessThan },
            {ValueComparingOperator.LessThanOrEqual, Expression.LessThanOrEqual },
            {ValueComparingOperator.StringContains, (memberExpression, constantExpression) =>
                Expression.Call(memberExpression, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constantExpression)},
            {ValueComparingOperator.CollectionContains, (memberExpression, contantExpression) => 
                Expression.Call(memberExpression, typeof(ICollection<>).GetMethod("Contains", new[]{ typeof(EntityBase) }), contantExpression)},
            
            //TODO: how to handle negative oposite?
            {ValueComparingOperator.CollectionDoesNotContain, (memberExpression, contantExpression) =>
                Expression.Not(Expression.Call(memberExpression, typeof(ICollection<>).GetMethod("Contains", new[]{ typeof(EntityBase) }), contantExpression))} 
        };

        /// <summary>
        /// Transforms predicates to a expression
        /// </summary>
        /// <param name="elementaryPredicate">An elementary predicate</param>
        /// <param name="parameterExpression">A parameter expression</param>
        /// <returns>An expresion</returns>
        public static Expression GetExpression(this ElementaryPredicate elementaryPredicate, ParameterExpression parameterExpression)
        {
            var memberExpression = Expression.PropertyOrField(parameterExpression, elementaryPredicate.TargetProperty);
            // Ensure compared value has the same type as the accessed member
            var memberType = GetMemberType(elementaryPredicate, memberExpression);
            var constantExpression = Expression.Constant(elementaryPredicate.ComparedValue, memberType);
            return TransformToExpression(elementaryPredicate.ValueComparingOperator, memberExpression, constantExpression);
        }

        /// <summary>
        /// Returns member type
        /// </summary>
        /// <param name="elementaryPredicate">An elementary predicate</param>
        /// <param name="memberExpression">A parameter expression</param>
        /// <returns>Member type</returns>
        private static Type GetMemberType(ElementaryPredicate elementaryPredicate, MemberExpression memberExpression)
        {
            return memberExpression.Member.DeclaringType?.GetProperty(elementaryPredicate.TargetProperty)?.PropertyType;
        }

        /// <summary>
        /// Transforms input to a new expression
        /// </summary>
        /// <param name="comparingOperator">Value comparing operator</param>
        /// <param name="memberExpression">Member expression</param>
        /// <param name="constantExpression">Constant expression</param>
        /// <returns>A LINQ expression</returns>
        private static Expression TransformToExpression(ValueComparingOperator comparingOperator, MemberExpression memberExpression, ConstantExpression constantExpression)
        {
            if (!Expressions.ContainsKey(comparingOperator))
            {
                throw new InvalidOperationException(string.Format(Exceptions.WLE007, comparingOperator));
            }
            return Expressions[comparingOperator].Invoke(memberExpression, constantExpression);
        }
    }
}
