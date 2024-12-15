using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;

public record ProductTypeAttributeDto : BaseDto
{
    public string Name { get; set; }
    public bool IsInherited { get; set; }
    public List<ProductTypeAttributeValueDto> Values { get; set; }
}
