using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.Products.Models;

public record AdminProductDto : BaseDto
{
    public string? Sku { get; set; }
    public string Name { get; set; } = null!;
    public int ProductTypeId { get; set; }
    public string? Description { get; set; }
    public bool IsBook { get; set; }
    public bool IsActive { get; set; }
    public int? UnitMeasureId { get; set; }
    public decimal? AverageRating { get; set; }
    public int RatingsCount { get; set; }
    public int TotalStockQuantity { get; set; }
    public AdminProductSentimentSummary? SentimentSummary { get; set; }

    // navigation properties
    public ProductTypeDto? ProductType { get; set; }
    public UnitMeasureDto? UnitMeasure { get; set; }
    public ICollection<ProductTypeAttributeProductValueDto> AttributeProductValues { get; set; } = null!;
    public ICollection<ProductVariantDto> ProductVariants { get; set; } = [];
    public ICollection<ProductImageDto> ProductImages { get; set; } = [];
}

public record AdminProductSentimentSummary
{
    public decimal? AverageSentimentScore { get; set; }
    public int TotalRatings { get; set; }
    public int PositiveRatings { get; set; }
    public int NegativeRatings { get; set; }
    public int NeutralRatings { get; set; }
    public string DominantSentiment { get; set; } = string.Empty;
    public decimal SentimentDistribution { get; set; }
    public List<AdminVariantSentimentDto> VariantSentiments { get; set; } = new();
}

public record AdminVariantSentimentDto
{
    public int ProductVariantId { get; set; }
    public string? VariantSku { get; set; }
    public decimal? AverageSentimentScore { get; set; }
    public int TotalRatings { get; set; }
    public int PositiveRatings { get; set; }
    public int NegativeRatings { get; set; }
    public int NeutralRatings { get; set; }
    public string DominantSentiment { get; set; } = string.Empty;
}