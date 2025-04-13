using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.Products.Models;

public record ProductVariantSummaryDto : BaseDto
{
    public string Sku { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal RecommendedRetailPrice { get; set; }
    public decimal DiscountRate => RecommendedRetailPrice == 0 ? 0 : (RecommendedRetailPrice - UnitPrice) / RecommendedRetailPrice * 100;
    public int StockQuantity { get; set; }
    public string ThumbnailImageUrl { get; set; } = string.Empty;
    public string StockStatus => StockQuantity > 0 ? "In Stock" : "Out of Stock";
    public IEnumerable<OptionValueDto>? OptionValues { get; set; }
    public IEnumerable<StockSummaryDto> StockBreakdowns { get; set; } = [];
}

public record StockSummaryDto
{
    public int BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
    public string Status => IsActive && StockQuantity > 0 ? "In Stock" : "Out of Stock";
}