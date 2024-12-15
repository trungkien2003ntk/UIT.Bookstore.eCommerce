using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.ProductTypeAttributes.GetProductTypeAttributeValues;

public record ProductTypeAttributeValueDto : BaseDto
{
    public int ProductTypeAttributeId { get; set; }
    public string Value { get; set; } = null!;
}