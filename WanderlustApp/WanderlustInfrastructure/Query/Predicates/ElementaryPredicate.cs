using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using WanderlustInfrastructure.Query.Predicates.Operators;
using WanderlustResource.Backend;

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

        public Expression BuildExpression(ParameterExpression parameterExpression)
        {
            var memberExpression = ExtractMemberExpression(parameterExpression);
            var memberType = GetMemberType(memberExpression);
            bool isEnumberable = ImplementsEnumberable(memberType);

            var constantExpression = isEnumberable ? Expression.Constant(ComparedValue) : Expression.Constant(ComparedValue, memberType);
            return TransformToExpression(ValueComparingOperator, memberExpression, constantExpression);
        }

        /// <summary>
        /// Extracts member expression from the parameter
        /// Copied from https://stackoverflow.com/a/16208620 and refactored
        /// </summary>
        /// <param name="parameterExpression">The name of the property. In case of nested property should be written
        /// in the format "Property.NestedProperty"</param>
        /// <returns>Member expression for the property</returns>
        private MemberExpression ExtractMemberExpression(ParameterExpression parameterExpression)
        {
            char delimiter = '.';
            Expression expression = parameterExpression;
            string[] propertyNamesChain = TargetProperty.Split(delimiter);

            foreach (var member in propertyNamesChain)
            {
                expression = Expression.PropertyOrField(parameterExpression, member);
            }
            return expression as MemberExpression;
        }

        /// <summary>
        /// Returns member type
        /// </summary>
        /// <param name="elementaryPredicate">An elementary predicate</param>
        /// <param name="memberExpression">A parameter expression</param>
        /// <returns>Member type</returns>
        private Type GetMemberType(MemberExpression memberExpression)
        {
            return memberExpression.Member.DeclaringType?.GetProperty(TargetProperty)?.PropertyType;
        }

        private bool ImplementsEnumberable(Type propertyType)
        {
            return propertyType.GetInterfaces().Contains(typeof(IEnumerable)) && !propertyType.Equals(typeof(string));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ParameterExpression CreateParameterAccordingToProperty()
        {
            return null;
        }

        /// <summary>
        /// Transforms input to a new expression
        /// </summary>
        /// <param name="comparingOperator">Value comparing operator</param>
        /// <param name="memberExpression">Member expression</param>
        /// <param name="constantExpression">Constant expression</param>
        /// <returns>A LINQ expression</returns>
        private Expression TransformToExpression(ValueComparingOperator comparingOperator, MemberExpression memberExpression,
                                                 ConstantExpression constantExpression)
        {
            if (!Expressions.ContainsKey(comparingOperator))
            {
                throw new InvalidOperationException(string.Format(Exceptions.WLE007, comparingOperator));
            }
            var callback = Expressions[comparingOperator];
            return callback.Invoke(memberExpression, constantExpression);
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
