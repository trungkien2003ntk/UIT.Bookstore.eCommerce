using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.Products.GetProductOptions;

public record ProductOptionsDto : BaseDto
{
    public string VariantName { get; set; } = null!;
    public ICollection<ProductOptionValueDto> Values { get; set; } = null!;

    public record ProductOptionValueDto : BaseDto
    {
        public string Value { get; set; } = null!;
    }
}