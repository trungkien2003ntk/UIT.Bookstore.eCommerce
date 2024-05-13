using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Products.Queries.GetProductDetail;

public record SkuDto : BaseDto
{
    public string SkuValue { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal RecommendedRetailPrice { get; set; }
    public string Barcode { get; set; }
    public decimal Quantity { get; set; }
    public decimal Status { get; set; }
    public IEnumerable<OptionValueDto>? OptionValues { get; set; }
    public IEnumerable<string> ThumbnailImageUrls { get; set; } = new List<string>();
    public IEnumerable<string> LargeImageUrls { get; set; } = new List<string>();
}

