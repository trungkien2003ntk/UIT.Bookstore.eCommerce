using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Products.Models;

public record ProductVariantDto : BaseDto
{
    public string SkuValue { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal RecommendedRetailPrice { get; set; }
    public decimal BasicDiscountRate { get; set; }
    public string Barcode { get; set; }
    public decimal Quantity { get; set; }
    public string Status { get; set; }
    public decimal Weight { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Length { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string LargeImageUrl { get; set; }
    public IEnumerable<OptionValueDto>? OptionValues { get; set; }
}

