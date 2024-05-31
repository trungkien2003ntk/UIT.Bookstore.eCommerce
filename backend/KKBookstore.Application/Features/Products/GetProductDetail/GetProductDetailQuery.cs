using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Products.Models;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Products.GetProductDetail;

public record GetProductDetailQuery(int ProductId) : IRequest<Result<ProductDetailDto>>;

public class GetProductDetailQueryHandler(
    IApplicationDbContext applicationDbContext,
    IMapper mapper
) : IRequestHandler<GetProductDetailQuery, Result<ProductDetailDto>>
{

    public async Task<Result<ProductDetailDto>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        // Query to fetch the product and related data
        var product = await applicationDbContext.Products
            .Where(p => p.IsActive && !p.IsDeleted && p.Id == request.ProductId)
            .Include(p => p.ProductType)
            .Include(p => p.UnitMeasure)
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            return Result.Failure<ProductDetailDto>(ProductErrors.NotFound);
        }

        var skus = await applicationDbContext.Skus
            .Where(s => s.ProductId == product.Id && s.IsActive)
            .Include(s => s.SkuOptionValues)
                .ThenInclude(so => so.Option)
            .Include(s => s.SkuOptionValues)
                .ThenInclude(so => so.OptionValue)
            .Include(s => s.SkuValue)
            .Include(s => s.Dimension)
            .ToListAsync(cancellationToken);

        product.Skus = skus;

        // Fetch Book Authors if the product is a book
        if (product.IsBook)
        {
            var bookAuthors = await applicationDbContext.BookAuthors
                .Include(ab => ab.Author)
                .Where(wb => wb.ProductId == product.Id)
                .ToListAsync(cancellationToken);

            product.BookAuthors = bookAuthors;
        }

        // Map to ProductDetailDto
        var productDto = mapper.Map<ProductDetailDto>(product);

        // Calculate min and max prices
        if (product.Skus.Any())
        {
            productDto.MinUnitPrice = product.Skus.Min(s => s.UnitPrice);
            productDto.MaxUnitPrice = product.Skus.Max(s => s.UnitPrice);
            productDto.MinRecommendedRetailPrice = product.Skus.Min(s => s.RecommendedRetailPrice);
            productDto.MaxRecommendedRetailPrice = product.Skus.Max(s => s.RecommendedRetailPrice);
        }

        return Result.Success(productDto);
    }
}
