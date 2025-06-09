using KKBookstore.Models;
using KKBookstore.Services;
using MediatR;

namespace KKBookstore.Features.ProductTypes.GetProductTypeAttributes;

public record GetProductTypeAttributesQuery(int ProductTypeId) : IRequest<Result<GetProductTypeAttributesResponse>>;

public class GetProductTypeAttributesQueryHandler(
    ProductTypeAttributeService productTypeAttributeService
) : IRequestHandler<GetProductTypeAttributesQuery, Result<GetProductTypeAttributesResponse>>
{
    public async Task<Result<GetProductTypeAttributesResponse>> Handle(GetProductTypeAttributesQuery request, CancellationToken cancellationToken)
    {
        var attributesResult = await productTypeAttributeService
            .GetProductTypeAttributesIncludingParents(request.ProductTypeId, cancellationToken);

        if (!attributesResult.IsSuccess)
            return Result.Failure<GetProductTypeAttributesResponse>(attributesResult.Error);

        // Group by Name and take the first attribute for each Name
        var uniqueAttributes = attributesResult.Value
                .GroupBy(x => x.Name)
                .Select(g =>
                {
                    var first = g.First();
                    // Merge values from all duplicates
                    first.Values = g.SelectMany(x => x.Values).Distinct().ToList();
                    return first;
                });

        var response = new GetProductTypeAttributesResponse
        {
            ListAttributes = uniqueAttributes.Select(x => new GetProductTypeAttributesResponse.ProductTypeAttributeDto
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
        };

        return response;
    }
}
