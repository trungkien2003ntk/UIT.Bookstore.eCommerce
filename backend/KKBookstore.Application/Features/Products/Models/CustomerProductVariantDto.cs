using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.Products.Models;

public record CustomerProductVariantDto : BaseDto
{
    public string? Sku { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal RecommendedRetailPrice { get; set; }
    public decimal BasicDiscountRate { get; set; }
    public string? Barcode { get; set; }
    public decimal StockQuantity { get; set; }
    public string? Status { get; set; }
    public decimal Weight { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Length { get; set; }
    public string? ThumbnailImageUrl { get; set; }
    public string? LargeImageUrl { get; set; }
    public decimal? AverageRating { get; set; }
    public int RatingsCount { get; set; }
    public VariantSentimentSummary? SentimentSummary { get; set; }
    public IEnumerable<OptionValueDto>? OptionValues { get; set; }
    public IEnumerable<StockBreakdownDto> StockBreakdowns { get; set; } = [];
    public IEnumerable<RatingDto> Ratings { get; set; } = [];
}

public record VariantSentimentSummary
{
    public decimal? AverageSentimentScore { get; set; }
    public int TotalRatings { get; set; }
    public int PositiveRatings { get; set; }
    public int NegativeRatings { get; set; }
    public int NeutralRatings { get; set; }
    public string DominantSentiment { get; set; } = string.Empty;
}

