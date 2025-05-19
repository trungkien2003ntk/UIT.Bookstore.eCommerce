using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.Products.Models;

public record ProductRatingDto : BaseAuditedDto
{
    public string Comment { get; init; }
    public int RatingValue { get; init; }
    public string UserName { get; init; }
    public string? UserAvatarUrl { get; init; }
    public string ProductVariantName { get; init; }
    public int LikesCount { get; init; }
    public int ReportsCount { get; init; }
    public string? Response { get; init; }
    public string Status { get; init; }
    public List<RatingImageDto>? Images { get; init; }
}

public record RatingImageDto : BaseDto
{
    public string ImageUrl { get; init; } = null!;
}