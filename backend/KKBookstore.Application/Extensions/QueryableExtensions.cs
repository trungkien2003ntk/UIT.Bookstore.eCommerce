using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace KKBookstore.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Conditionally applies a Where clause to an IQueryable if the specified condition is true.
    /// </summary>
    /// <typeparam name="T">The type of elements in the query</typeparam>
    /// <param name="source">The source queryable object</param>
    /// <param name="condition">The condition that determines whether to apply the predicate</param>
    /// <param name="predicate">The predicate to apply when the condition is true</param>
    /// <returns>The filtered queryable if condition is true; otherwise the original queryable</returns>
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }

    /// <summary>
    /// Conditionally applies a Where clause to an IQueryable if the specified condition is not null.
    /// </summary>
    /// <typeparam name="T">The type of elements in the query</typeparam>
    /// <typeparam name="TValue">The type of the condition value</typeparam>
    /// <param name="source">The source queryable object</param>
    /// <param name="conditionValue">The value to check for null</param>
    /// <param name="predicateBuilder">A function that builds a predicate using the non-null condition value</param>
    /// <returns>The filtered queryable if conditionValue is not null; otherwise the original queryable</returns>
    public static IQueryable<T> WhereIf<T, TValue>(
        this IQueryable<T> source,
        TValue? conditionValue,
        Func<TValue, Expression<Func<T, bool>>> predicateBuilder)
        where TValue : struct
    {
        return conditionValue.HasValue ? source.Where(predicateBuilder(conditionValue.Value)) : source;
    }

    public static async Task<Result<PagedResult<T>>> SortAndPaginateWithResultAsync<T>(
        this IQueryable<T> query,
        string sortBy,
        string sortDirection,
        List<string> validSortProperties,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
        where T : class
    {
        // Apply sorting
        try
        {
            query = query.OrderBy(sortBy, sortDirection, validSortProperties);
        }
        catch
        {
            return Result.Failure<PagedResult<T>>(Error.InvalidSortProperty(sortBy, string.Join(',', validSortProperties)));
        }

        // Create paginated result
        var totalItemsCount = await query.CountAsync(cancellationToken);

        // Apply pagination
        var paginatedItems = await query.PaginateAsync(pageNumber, pageSize, cancellationToken);

        var result = new PagedResult<T>(paginatedItems, totalItemsCount, pageSize, pageNumber);

        return result;
    }

    public static async Task<PagedResult<T>> SortAndPaginateAsync<T>(
        this IQueryable<T> query,
        string sortBy,
        string sortDirection,
        List<string> validSortProperties,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
        where T : class
    {
        // Apply sorting
        try
        {
            query = query.OrderBy(sortBy, sortDirection, validSortProperties);
        }
        catch
        {
            throw new ArgumentException($"Invalid sort property '{sortBy}'. Valid properties are: {string.Join(',', validSortProperties)}");
        }
        // Get total count before pagination
        var totalItemsCount = await query.CountAsync(cancellationToken);

        // Apply pagination
        var paginatedItems = await query.PaginateAsync(pageNumber, pageSize, cancellationToken);
        var result = new PagedResult<T>(paginatedItems, totalItemsCount, pageSize, pageNumber);

        return result;
    }

    public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string propertyName, string sortDirection, IEnumerable<string> validProperties)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            var defaultProperty = "Id";

            return sortDirection == "asc"
                ? query.OrderBy($"{defaultProperty}")
                : query.OrderByDescending($"{defaultProperty}");
        }

        if (!validProperties.Contains(propertyName))
        {
            throw new ArgumentException($"'{propertyName}' is not a valid sort property of '{typeof(T).FullName}'");
        }

        var propertyInfo = typeof(T).GetProperty(propertyName);
        if (propertyInfo == null)
        {
            throw new ArgumentException($"'{propertyName}' is not a valid property of '{typeof(T).FullName}'");
        }

        return sortDirection == "asc"
            ? query.OrderBy($"{propertyName}")
            : query.OrderByDescending($"{propertyName}");
    }

    public static IOrderedQueryable<TSource> OrderBy<TSource>(
       this IQueryable<TSource> query, string propertyName)
    {
        var entityType = typeof(TSource);

        //Create x=>x.PropName
        var propertyInfo = entityType.GetProperty(propertyName);
        ParameterExpression arg = Expression.Parameter(entityType, "x");
        MemberExpression property = Expression.Property(arg, propertyName);
        var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

        //Get System.Linq.Queryable.OrderBy() method.
        var enumarableType = typeof(Queryable);
        var method = enumarableType.GetMethods()
             .Where(m => m.Name == "OrderBy" && m.IsGenericMethodDefinition)
             .Where(m =>
             {
                 var parameters = m.GetParameters().ToList();
                 //Put more restriction here to ensure selecting the right overload                
                 return parameters.Count == 2;//overload that has 2 parameters
             }).Single();
        //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
        MethodInfo genericMethod = method
             .MakeGenericMethod(entityType, propertyInfo.PropertyType);

        /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
          Note that we pass the selector as Expression to the method and we don't compile it.
          By doing so EF can extract "order by" columns and generate SQL for it.*/
        var newQuery = (IOrderedQueryable<TSource>)genericMethod
             .Invoke(genericMethod, new object[] { query, selector });
        return newQuery;
    }

    public static IOrderedQueryable<TSource> OrderByDescending<TSource>(
       this IQueryable<TSource> query, string propertyName)
    {
        var entityType = typeof(TSource);

        //Create x=>x.PropName
        var propertyInfo = entityType.GetProperty(propertyName);
        ParameterExpression arg = Expression.Parameter(entityType, "x");
        MemberExpression property = Expression.Property(arg, propertyName);
        var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

        //Get System.Linq.Queryable.OrderBy() method.
        var enumarableType = typeof(Queryable);
        var method = enumarableType.GetMethods()
             .Where(m => m.Name == "OrderByDescending" && m.IsGenericMethodDefinition)
             .Where(m =>
             {
                 var parameters = m.GetParameters().ToList();
                 //Put more restriction here to ensure selecting the right overload                
                 return parameters.Count == 2;//overload that has 2 parameters
             }).Single();
        //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
        MethodInfo genericMethod = method
             .MakeGenericMethod(entityType, propertyInfo.PropertyType);

        /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
          Note that we pass the selector as Expression to the method and we don't compile it.
          By doing so EF can extract "order by" columns and generate SQL for it.*/
        var newQuery = (IOrderedQueryable<TSource>)genericMethod
             .Invoke(genericMethod, new object[] { query, selector });
        return newQuery;
    }


    public static async Task<List<T>> PaginateAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}
