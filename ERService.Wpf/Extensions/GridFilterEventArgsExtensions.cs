using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ERService.Wpf.Extensions
{
    public static class GridFilterEventArgsExtensions
    {
        private static readonly MethodInfo strContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        private static readonly MethodInfo strStartsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private static readonly MethodInfo strEndsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
        private delegate Expression Binder(Expression left, Expression right);

        private static Expression ParseTree(GridFilterEventArgs filterEventArgs, ParameterExpression parm)
        {
            Expression left = null;
            foreach (var predicate in filterEventArgs.FilterPredicates)
            {
                var gate = predicate.PredicateType;
                Binder binder = gate switch
                {
                    PredicateType.And => Expression.And,
                    PredicateType.AndAlso => Expression.AndAlso,
                    PredicateType.Or => Expression.Or,
                    PredicateType.OrElse => Expression.OrElse,
                    _ => throw new InvalidOperationException()
                };

                Expression bind(Expression left, Expression right) =>
                    left == null ? right : binder(left, right);

                var property = Expression.Property(parm, filterEventArgs.Column.MappingName);
                var value = Expression.Constant(predicate.FilterValue);

                Expression right = predicate.FilterType switch
                {
                    FilterType.Equals => Expression.Equal(property, value),
                    FilterType.NotEquals => Expression.NotEqual(property, value),
                    FilterType.Contains => Expression.Call(property, strContainsMethod, value),
                    FilterType.NotContains => Expression.Not(Expression.Call(property, strContainsMethod, value)),
                    FilterType.StartsWith => Expression.Call(property, strStartsWithMethod, value),
                    FilterType.NotStartsWith => Expression.Not(Expression.Call(property, strStartsWithMethod, value)),
                    FilterType.EndsWith => Expression.Call(property, strEndsWithMethod, value),
                    FilterType.NotEndsWith => Expression.Not(Expression.Call(property, strEndsWithMethod, value)),
                    _ => throw new InvalidOperationException()
                };

                left = bind(left, right);
            }

            return left;
        }

        public static Expression<Func<T, bool>> ToExpressionOf<T>(this GridFilterEventArgs filterEventArgs)
        {
            if (filterEventArgs.FilterPredicates == null)
                return T => true;

            var itemExpression = Expression.Parameter(typeof(T));
            var conditions = ParseTree(filterEventArgs, itemExpression);
            if (conditions.CanReduce)
                conditions = conditions.ReduceAndCheck();

            return Expression.Lambda<Func<T, bool>>(conditions, itemExpression);
        }

        public static Func<T, bool> ToPredicateOf<T>(this GridFilterEventArgs filterEventArgs) => 
            ToExpressionOf<T>(filterEventArgs).Compile();
    }
}