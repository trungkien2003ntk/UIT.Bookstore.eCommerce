using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Features.Products.Models;

namespace KKBookstore.Application.Features.Products.GetProductRatingList;

public record ProductRatingSummary
{
    public decimal AverageRating { get; init; }
    public int TotalApprovedRating { get; init; }
    public int TotalRating { get; init; }
    public int Total5StarRating { get; init; }
    public int Total4StarRating { get; init; }
    public int Total3StarRating { get; init; }
    public int Total2StarRating { get; init; }
    public int Total1StarRating { get; init; }
    public int TotalRatingWithComment { get; init; }
    public int TotalRatingWithImage { get; init; }
    public PaginatedResult<ProductRatingDto> Ratings { get; init; }
}
