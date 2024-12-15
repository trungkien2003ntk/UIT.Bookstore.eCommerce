using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace KKBookstore.Application.Extensions;

public static class QueryableExtensions
{
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

        // Apply pagination
        var paginatedItems = await query.PaginateAsync(pageNumber, pageSize, cancellationToken);

        // Create paginated result
        var totalItemsCount = await query.CountAsync(cancellationToken);
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
        var enumarableType = typeof(System.Linq.Queryable);
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
        var enumarableType = typeof(System.Linq.Queryable);
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
