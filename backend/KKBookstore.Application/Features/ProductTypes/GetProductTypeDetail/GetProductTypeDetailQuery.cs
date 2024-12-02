using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.ProductTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static KKBookstore.Application.Features.ProductTypes.GetProductTypeDetail.GetProductTypeDetailResponse;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeDetail;

public record GetProductTypeDetailQuery(int ProductTypeId, bool WithParent = true) : IRequest<Result<GetProductTypeDetailResponse>>;

public class GetProductTypeDetailQueryHandler(IApplicationDbContext dbContext)
    : IRequestHandler<GetProductTypeDetailQuery, Result<GetProductTypeDetailResponse>>
{
    public async Task<Result<GetProductTypeDetailResponse>> Handle(GetProductTypeDetailQuery request, CancellationToken cancellationToken)
    {
        var productType = await dbContext.ProductTypes
            .Where(pt => pt.Id == request.ProductTypeId)
            .FirstOrDefaultAsync(cancellationToken);

        if (productType is null)
        {
            return Result.Failure<GetProductTypeDetailResponse>(ProductTypeErrors.NotFound);
        }

        var allProductTypes = await dbContext.ProductTypes
            .ToListAsync(cancellationToken);

        var productTypeLookup = allProductTypes.ToLookup(pt => pt.ParentProductTypeId);
        var result = new GetProductTypeDetailResponse() 
        { 
            ListItem = [
                new() {
                    Id = productType.Id,
                    ProductTypeCode = productType.ProductTypeCode,
                    Level = productType.Level,
                    DisplayName = productType.DisplayName,
                    Description = productType.Description,
                    ParentProductTypeId = productType.ParentProductTypeId,
                    ChildProductTypes = null
                }
            ]
        };

        if (request.WithParent)
        {
            while (productType.ParentProductTypeId != null)
            {
                var parentProductType = allProductTypes.Where(pt => pt.Id == productType.ParentProductTypeId).First();

                result.ListItem.Add(new ProductTypeGeneralDto()
                {
                    Id = parentProductType.Id,
                    ProductTypeCode = parentProductType.ProductTypeCode,
                    Level = parentProductType.Level,
                    DisplayName = parentProductType.DisplayName,
                    Description = parentProductType.Description,
                    ParentProductTypeId = parentProductType.ParentProductTypeId,
                    ChildProductTypes = null
                });

                productType = parentProductType;
            }

            // sort by item's level
            result.ListItem.Sort(Comparer<ProductTypeGeneralDto>.Create((x, y) => x.Level.CompareTo(y.Level)));
        }

        return result;
    }
}