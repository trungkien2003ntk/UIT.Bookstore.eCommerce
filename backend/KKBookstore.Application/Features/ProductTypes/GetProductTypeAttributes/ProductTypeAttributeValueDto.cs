using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;

public record ProductTypeAttributeValueDto : BaseDto
{
    public int ProductTypeAttributeId { get; set; }
    public string Value { get; set; }
}
