using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Products;

namespace KKBookstore.Features.Ratings.Models;

public record RatingDetailDto : BaseAuditedDto
{
    public string Comment { get; init; } = string.Empty;
    public int RatingValue { get; init; }
    public int ProductId { get; init; }
    public int ProductVariantId { get; init; }
    public string ProductVariantName { get; init; } = string.Empty;
    public int CustomerId { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string? UserAvatarUrl { get; init; }
    public int LikesCount { get; init; }
    public int ReportsCount { get; init; }
    public string? Response { get; init; }
    public RatingStatus Status { get; init; }
    public List<string>? ImageUrls { get; init; }

    // AI Moderation properties
    public int? AiModerationScore { get; init; }
    public string? AiModerationCategory { get; init; }
    public string? AiModerationExplanation { get; init; }
    public DateTimeOffset? AiModerationDate { get; init; }
    public bool IsAiModerated { get; init; }

    // Related entities
    public ProductDetailInfoDto? Product { get; init; }
    public List<RatingLikeDto>? Likes { get; init; }
    public List<RatingReportDto>? Reports { get; init; }
}

public record ProductDetailInfoDto : BaseDto
{
    public string Name { get; init; } = string.Empty;
    public string? ThumbnailImageUrl { get; init; }
    public decimal AverageRating { get; init; }
    public int TotalRatingsCount { get; init; }
}

public record RatingLikeDto : BaseAuditedDto
{
    public int CustomerId { get; init; }
    public string CustomerUserName { get; init; } = string.Empty;
    public string CustomerFullName { get; init; } = string.Empty;
    public bool Liked { get; init; }
}

public record RatingReportDto : BaseAuditedDto
{
    public int CustomerId { get; init; }
    public string CustomerUserName { get; init; } = string.Empty;
    public string CustomerFullName { get; init; } = string.Empty;
    public string Reason { get; init; } = string.Empty;
    public string? DetailedReason { get; init; }
}
