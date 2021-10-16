using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WanderlustResource.Backend;
using WanderlustInfrastructure.Query.Predicates.Operators;
using WanderlustInfrastructure.Entity;
using System.Collections;
using System.Linq;
using System.Reflection;

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
                Expression.Call(memberExpression, typeof(string)
                          .GetMethod("Contains", new[] { typeof(string) }), constantExpression)},

            {ValueComparingOperator.In, (memberExpression, constantExpression) => 
                                            Expression.Call(typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                                                                              .Single(m => m.Name == nameof(Enumerable.Contains)
                                                                               && m.GetParameters().Length == 2)
                                                                              .MakeGenericMethod(constantExpression.Type),
                                         memberExpression, constantExpression)},
            
            {ValueComparingOperator.NotIn, (memberExpression, constantExpression) =>
                                            Expression.Not(Expression.Call(typeof(Enumerable).GetMethods(BindingFlags.Static | 
                                                                                                         BindingFlags.Public)
                                                                              .Single(m => m.Name == nameof(Enumerable.Contains)
                                                                               && m.GetParameters().Length == 2)
                                                                              .MakeGenericMethod(constantExpression.Type),
                                            memberExpression, constantExpression))}
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
            var memberType = GetMemberType(elementaryPredicate, memberExpression);
            bool isEnumberable = ImplementsEnumberable(memberType);

            var constantExpression = isEnumberable ? Expression.Constant(elementaryPredicate.ComparedValue) :
                                                    Expression.Constant(elementaryPredicate.ComparedValue, memberType);

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

        public static bool ImplementsEnumberable(Type propertyType)
        {
            return propertyType.GetInterfaces().Contains(typeof(IEnumerable)) && !propertyType.Equals(typeof(string));
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
            var callback = Expressions[comparingOperator];
            return callback.Invoke(memberExpression, constantExpression);
        }
    }
}
