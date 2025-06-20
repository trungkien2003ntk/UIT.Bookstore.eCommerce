using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Products;

namespace KKBookstore.Features.Ratings.Models;

public record RatingDto : BaseAuditedDto
{
    public string Comment { get; init; } = string.Empty;
    public int RatingValue { get; init; }
    public int ProductId { get; init; }
    public int ProductVariantId { get; init; }
    public string ProductVariantName { get; init; } = string.Empty;
    public int? CustomerId { get; init; }
    public string? UserName { get; init; }
    public string? FullName { get; init; }
    public string? UserAvatarUrl { get; init; }
    public int LikesCount { get; init; }
    public int ReportsCount { get; init; }
    public string? Response { get; init; }
    public RatingStatus Status { get; init; }
    public List<string>? ImageUrls { get; init; }
    public ProductBasicInfoDto? Product { get; init; }
}

public record ProductBasicInfoDto : BaseDto
{
    public string Name { get; init; } = string.Empty;
    public string? ThumbnailImageUrl { get; init; }
    public decimal AverageRating { get; init; }
}