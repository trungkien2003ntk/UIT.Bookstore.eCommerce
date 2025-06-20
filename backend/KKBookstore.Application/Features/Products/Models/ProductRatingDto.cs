using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Products;

namespace KKBookstore.Features.Products.Models;

public record ProductRatingDto : BaseAuditedDto
{
    public string Comment { get; init; }
    public int RatingValue { get; init; }
    public int? CustomerId { get; init; }
    public string UserName { get; init; }
    public string FullName { get; init; }
    public string? UserAvatarUrl { get; init; }
    public string ProductVariantName { get; init; }
    public int LikesCount { get; init; }
    public int ReportsCount { get; init; }
    public string? Response { get; init; }
    public RatingStatus Status { get; init; }
    public List<string>? ImageUrls { get; init; }
}