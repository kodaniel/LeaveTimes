using System.Linq.Expressions;

namespace LeaveTimes.Infrastructure.Utils;

internal static class QueryableExtensions
{
    /// <summary>
    /// Conditional Where.
    /// </summary>
    /// <typeparam name="TSource">Type which will be used on Where.</typeparam>
    /// <param name="query">Query.</param>
    /// <param name="condition">Condition, if true then add the filter to the query.</param>
    /// <param name="predicate">Where predicate.</param>
    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> query, 
        bool condition, Expression<Func<TSource, bool>> predicate)
    {
        return condition ? query.Where(predicate) : query;
    }
}
