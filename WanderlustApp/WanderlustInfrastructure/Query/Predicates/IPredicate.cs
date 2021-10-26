using System.Linq.Expressions;

namespace WanderlustInfrastructure.Query.Predicates
{
    /// <summary>
    /// An interface for a predicate
    /// </summary>
    public interface IPredicate
    {
        /// <summary>
        /// Builds a LINQ Expression from the predicate
        /// </summary>
        /// <param name="parameterExpression">Parameter expression</param>
        /// <returns>LINQ expression</returns>
        Expression BuildExpression(ParameterExpression parameterExpression);
    }
}
