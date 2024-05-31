using KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeList;

public record GetProductTypeListResponse
{
    public List<ProductTypeGeneralDto> ListItem { get; init; }
}
