using KKBookstore.Application.Features.ProductTypes.GetProductTypeList;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;

public class GetProductTypeAttributesResponse
{
    public List<ProductTypeAttributeDto> ListAttributes { get; init; }
}
