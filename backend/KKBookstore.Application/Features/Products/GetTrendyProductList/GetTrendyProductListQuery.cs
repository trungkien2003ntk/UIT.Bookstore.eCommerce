using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Products.Models;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Products.GetTrendyProductList;

public record GetTrendyProductListQuery(int? ProductTypeId = default) : IRequest<Result<List<ProductSummary>>>;

// todo: considering merge this query with GetProductListQuery
public class GetTrendyProductListQueryHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetTrendyProductListQuery, Result<List<ProductSummary>>>
{
    public async Task<Result<List<ProductSummary>>> Handle(GetTrendyProductListQuery request, CancellationToken cancellationToken)
    {
        // now as initialize, return a list of random products
        var numberOfTrendyProducts = 12;
        var productsQuery = dbContext.Products
            .OrderBy(p => Guid.NewGuid())
            .Take(numberOfTrendyProducts)
            .Include(p => p.Skus)
            .Include(p => p.Ratings)
            .AsQueryable();

        if (request.ProductTypeId.HasValue)
        {
            productsQuery = productsQuery.Where(p => p.ProductTypeId == request.ProductTypeId.Value);
        }

        var products = await productsQuery.ToListAsync(cancellationToken);

        var productGeneralDtos = mapper.Map<List<ProductSummary>>(products);

        return productGeneralDtos;
    }
}