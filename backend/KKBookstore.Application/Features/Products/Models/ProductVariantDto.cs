using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Products;

namespace KKBookstore.Features.Products.Models;

public sealed record ProductVariantDto : BaseDto
{
    public string? Sku { get; set; }
    public decimal RecommendedRetailPrice { get; set; }
    public decimal UnitPrice { get; set; }
    public int Weight { get; set; }
    public Dimension? Dimension { get; set; }
    public decimal TaxRate { get; set; }
    public string? Comment { get; set; }
    public int StockQuantity { get; set; }
    public int TotalQuantity { get; set; }
    public decimal? AverageRating { get; set; }
    public int RatingsCount { get; set; }

    public ICollection<VariantOptionDto> VariantOptions { get; set; } = [];
    public ICollection<StockBreakdownDto> StockBreakdowns { get; set; } = [];
    public ICollection<RatingDto> Ratings { get; set; } = []; public sealed record VariantOptionDto
    {
        public int ProductOptionId { get; set; }
        public int? ProductOptionValueId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}