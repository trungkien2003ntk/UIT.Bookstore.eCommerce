using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Products.Queries.GetProductList;
using KKBookstore.Domain.Common;
using KKBookstore.Domain.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Products.Queries.GetProductDetail;

public class GetProductDetailQueryHandler(
    IApplicationDbContext applicationDbContext,
    IMapper mapper
) : IRequestHandler<GetProductDetailQuery, Result<ProductDetailDto>>
{

    public async Task<Result<ProductDetailDto>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        var product = await applicationDbContext.Products
            .Include(p => p.ProductType)
            .Include(p => p.Skus)
                .ThenInclude(s => s.SkuOptionValues)
                    .ThenInclude(s => new { s.Option, s.OptionValue })
            .Include(p => p.UnitMeasure)
            .Where(p => p.IsActive && p.IsDeleted)
            .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
        
        if (product is null)
        {
            return Result.Failure<ProductDetailDto>(ProductErrors.NotFound);
        }

        if (product is not null && product.IsBook)
        {
            var bookAuthors = await applicationDbContext.BookAuthors
                .Include(ab => ab.Author)
                .Where(wb => wb.ProductId == product.Id)
                .ToListAsync(cancellationToken);

            product.BookAuthors = bookAuthors;
        }

        // get the product detail dto's min price, max price from sku prices

        var productDto = mapper.Map<ProductDetailDto>(product);

        productDto.MinUnitPrice = product!.Skus.Min(s => s.UnitPrice);
        productDto.MaxUnitPrice = product!.Skus.Max(s => s.UnitPrice);
        productDto.MinRecommendedRetailPrice = product!.Skus.Min(s => s.RecommendedRetailPrice);
        productDto.MaxRecommendedRetailPrice = product!.Skus.Max(s => s.RecommendedRetailPrice);

        return Result.Success(productDto);
    }
}
