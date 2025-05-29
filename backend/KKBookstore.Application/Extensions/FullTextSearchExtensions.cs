namespace KKBookstore.Extensions;

using KKBookstore.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

public static class FullTextSearchExtensions
{
    public static IQueryable<T> ApplyFullTextSearch<T>(
        this IQueryable<T> query,
        string? searchTerm,
        FullTextSearchMode searchMode = FullTextSearchMode.Any,
        Expression<Func<T, string>>[]? fullTextFields = null,
        Expression<Func<T, string>>[]? likeFields = null)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return query;

        string formattedSearchText = searchMode switch
        {
            FullTextSearchMode.All => SearchTextFormatter.FormatSearchTextWithAnd(searchTerm),
            _ => SearchTextFormatter.FormatSearchTextWithOr(searchTerm)
        };

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression? predicate = null;

        // Full-Text Fields
        if (fullTextFields != null)
        {
            foreach (var field in fullTextFields)
            {
                var body = Expression.Invoke(field, parameter);
                var containsMethod = typeof(SqlServerDbFunctionsExtensions).GetMethod(nameof(SqlServerDbFunctionsExtensions.Contains),
                    new[] { typeof(DbFunctions), typeof(string), typeof(string) })!;

                var call = Expression.Call(
                    containsMethod,
                    Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions))!),
                    body,
                    Expression.Constant(formattedSearchText));

                predicate = predicate == null ? call : Expression.OrElse(predicate, call);
            }
        }

        // LIKE Fields
        if (likeFields != null)
        {
            foreach (var field in likeFields)
            {
                var body = Expression.Invoke(field, parameter);
                var likeMethod = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like),
                    new[] { typeof(DbFunctions), typeof(string), typeof(string) })!;

                var call = Expression.Call(
                    likeMethod,
                    Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions))!),
                    body,
                    Expression.Constant($"%{searchTerm}%"));

                predicate = predicate == null ? call : Expression.OrElse(predicate, call);
            }
        }

        if (predicate == null)
            return query;

        var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
        return query.Where(lambda);
    }
}

public enum FullTextSearchMode
{
    /// <summary>Match any of the search terms (OR logic)</summary>
    Any,
    /// <summary>Match all search terms (AND logic)</summary>
    All
}
