using KKBookstore.Application.Services;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;

public record GetProductTypeAttributesQuery(int ProductTypeId) : IRequest<Result<GetProductTypeAttributesResponse>>;

public class GetProductTypeAttributesQueryHandler(
    ProductTypeAttributeService productTypeAttributeService
) : IRequestHandler<GetProductTypeAttributesQuery, Result<GetProductTypeAttributesResponse>>
{
    public async Task<Result<GetProductTypeAttributesResponse>> Handle(GetProductTypeAttributesQuery request, CancellationToken cancellationToken)
    {
        var attributesResult = await productTypeAttributeService
            .GetProductTypeAttributesIncludingParents(request.ProductTypeId, cancellationToken);

        return attributesResult.IsSuccess
            ? new GetProductTypeAttributesResponse
            {
                ListAttributes = attributesResult.Value.Select(x => new GetProductTypeAttributesResponse.ProductTypeAttributeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsInherited = x.IsInherited,
                    Values = x.Values.Select(v => new GetProductTypeAttributesResponse.ProductTypeAttributeDto.ProductTypeAttributeValueDto
                    {
                        AttributeId = v.ProductTypeAttributeId,
                        AttributeValueId = v.Id,
                        Name = x.Name,
                        Value = v.Value
                    }).ToList()
                }).ToList()
            }
            : Result.Failure<GetProductTypeAttributesResponse>(attributesResult.Error);
    }
}
