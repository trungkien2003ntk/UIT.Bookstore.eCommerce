using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Domain.Common;
using KKBookstore.Domain.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Products.Queries.GetProductList;

public class GetProductListQueryHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetProductListQuery, Result<PaginatedResult<ProductGeneralDto>>>
{
    public async Task<Result<PaginatedResult<ProductGeneralDto>>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {

        IQueryable<Product> query = dbContext.Products;

        if (request.ProductTypeId.HasValue)
        {
            // filter by product type
            query = query.Where(p => p.ProductTypeId == request.ProductTypeId.Value);
        }

        // Apply sorting
        var validSortProperties = new List<string> { "Name", "Id" }; 

        string sortProperty = request.SortBy;
        string sortDirection = request.SortDirection;
        if (!string.IsNullOrEmpty(sortProperty))
        {
            if (!validSortProperties.Contains(sortProperty))
            {
                return Result.Failure<PaginatedResult<ProductGeneralDto>>(ProductErrors.InvalidSortProperty(sortProperty, string.Join(", ", validSortProperties)));
            }

            var propertyInfo = typeof(Product).GetProperty(sortProperty);
            if (propertyInfo != null)
            {
                query = sortDirection == "asc"
                    ? query.OrderBy(p => propertyInfo.GetValue(p, null))
                    : query.OrderByDescending(p => propertyInfo.GetValue(p, null));
            }
        }

        // Apply pagination
        int pageSize = request.PageSize;
        int pageNumber = request.PageNumber;

        var paginatedProducts = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        // Map the paginated products to ProductGeneralDto objects
        var mappedProducts = mapper.Map<List<ProductGeneralDto>>(paginatedProducts);

        var result = new PaginatedResult<ProductGeneralDto>(
            mappedProducts,
            await query.CountAsync(cancellationToken),
            pageSize,
            pageNumber
        );

        return Result.Success(result);
    }
}
