using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.Products.Models;

public record ProductRatingDto : BaseDto
{
    public string Comment { get; init; }
    public int RatingValue { get; init; }
    public string UserName { get; init; }
    public string? UserAvatarUrl { get; init; }
    public string SkuName { get; init; }
    public int LikesCount { get; init; }
    public int ReportedCount { get; init; }
    public string? Response { get; init; }
    public string Status { get; init; }
}
