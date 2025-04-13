using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.ProductTypes.GetProductTypeAttributes;

public record ProductTypeAttributeValueDto : BaseDto
{
    public string Value { get; set; }
    public int ProductTypeAttributeId { get; set; }
}
