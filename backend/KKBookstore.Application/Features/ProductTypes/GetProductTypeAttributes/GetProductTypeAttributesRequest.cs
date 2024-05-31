using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.ProductTypes.GetProductTypeList;
using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;

public record GetProductTypeAttributesRequest(int ProductTypeId) : IRequest<Result<GetProductTypeAttributesResponse>>;

public class GetProductTypeAttributesQueryHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetProductTypeAttributesRequest, Result<GetProductTypeAttributesResponse>>
{
    public async Task<Result<GetProductTypeAttributesResponse>> Handle(GetProductTypeAttributesRequest request, CancellationToken cancellationToken)
    {
        var productTypeId = request.ProductTypeId;
        var productType = await dbContext.ProductTypes
            .Where(pt => pt.Id == productTypeId)
            .Include(pt => pt.ParentProductType)
            .FirstOrDefaultAsync(cancellationToken);

        if (productType is null)
        {
            return Result.Failure<GetProductTypeAttributesResponse>(ProductTypeErrors.NotFound);
        }

        var productTypeAttributesResult = new HashSet<ProductTypeAttributeDto>();
        var productTypeAttributes = await dbContext.ProductTypeAttributeMappings
            .Where(ptam => ptam.ProductTypeId == productTypeId)
            .Include(ptam => ptam.ProductAttribute)
            .ThenInclude(pa => pa.Values)
            .Include(ptam => ptam.ProductType)
            .ToListAsync(cancellationToken);

        productTypeAttributesResult.UnionWith(productTypeAttributes.Select(mapper.Map<ProductTypeAttributeDto>));

        if (productType.ParentProductType is null && productTypeAttributesResult.Count != 0)
        {
            return new GetProductTypeAttributesResponse { ListAttributes = [.. productTypeAttributesResult] };
        }

        // add other parent's attributes

        for (int i = productType.Level; i > 1; i--)
        {
            var parentAttributes = await dbContext.ProductTypeAttributeMappings
                .Where(ptam => ptam.ProductTypeId == productType.ParentProductTypeId)
                .Include(ptam => ptam.ProductAttribute)
                .ThenInclude(pa => pa.Values)
                .Include(ptam => ptam.ProductType)
                .ThenInclude(pt => pt.ParentProductType)
                .ToListAsync(cancellationToken);

            productTypeAttributesResult.UnionWith(parentAttributes.Select(mapper.Map<ProductTypeAttributeDto>));

            productType = productType.ParentProductType;
        }

        if (productTypeAttributesResult.Count == 0)
        {
            return Result.Failure<GetProductTypeAttributesResponse>(ProductTypeErrors.NotFound);
        }

        return new GetProductTypeAttributesResponse { ListAttributes = [.. productTypeAttributesResult] };
    }
}
