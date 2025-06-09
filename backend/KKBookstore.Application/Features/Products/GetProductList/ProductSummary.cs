using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Features.Products.Models;

namespace KKBookstore.Features.Products.GetProductList;

public record ProductSummary : BaseDto
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; }
    public int ProductTypeId { get; set; }
    public string ProductTypeName { get; set; }
    public string Description { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public bool IsBook { get; set; }
    public int SoldCount { get; set; }
    public decimal MinUnitPrice { get; set; }
    public decimal MinRecommendedRetailPrice { get; set; }
    public decimal MinDiscountRate => MinRecommendedRetailPrice == 0 ? 0 : (MinRecommendedRetailPrice - MinUnitPrice) / MinRecommendedRetailPrice * 100;
    public DateTimeOffset? CreationTime { get; set; }
    public decimal AverageRating { get; set; }
    public decimal RatingsCount { get; set; }
    public bool IsActive { get; set; }
    public int TotalStockQuantity { get; set; }
    public string StockStatus => TotalStockQuantity > 0 ? "In Stock" : "Out of Stock";
    public ICollection<ProductVariantSummaryDto> Variants { get; set; } = [];
}
