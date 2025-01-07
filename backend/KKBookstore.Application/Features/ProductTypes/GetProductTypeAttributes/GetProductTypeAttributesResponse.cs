using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;

public class GetProductTypeAttributesResponse
{
    public List<ProductTypeAttributeDto> ListAttributes { get; init; }

    public record ProductTypeAttributeDto : BaseDto
    {
        public string Name { get; set; }
        public bool IsInherited { get; set; }

        public List<ProductTypeAttributeValueDto> Values { get; set; }

        public record ProductTypeAttributeValueDto
        {
            public int AttributeId { get; set; }
            public int AttributeValueId { get; set; }
            public string Name { get; set; } = null!;
            public string Value { get; set; } = null!;
        }
    }
}
