using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Ratings.Models;
using KKBookstore.Mappings.Helpers;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Ratings.GetRatingDetail;

public record GetRatingDetailQuery(int RatingId) : IRequest<Result<RatingDetailDto>>;

public class GetRatingDetailQueryHandler : IRequestHandler<GetRatingDetailQuery, Result<RatingDetailDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetRatingDetailQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<RatingDetailDto>> Handle(GetRatingDetailQuery request, CancellationToken cancellationToken)
    {
        var rating = await _dbContext.Ratings
            .AsNoTracking()
            .Where(r => r.Id == request.RatingId)
            .Include(r => r.Customer)
            .Include(r => r.ProductVariant)
                .ThenInclude(pv => pv.ProductVariantOptionValues)!
                    .ThenInclude(x => x.OptionValue)
            .Include(r => r.ProductVariant)
                .ThenInclude(pv => pv.Product)
                    .ThenInclude(p => p.ProductImages)
            .Include(r => r.Images)
            .Include(r => r.Likes)
                .ThenInclude(l => l.Customer)            .Include(r => r.Reports)
            .FirstOrDefaultAsync(cancellationToken);

        if (rating is null)
        {
            return Result.Failure<RatingDetailDto>(ProductErrors.RatingNotFound);
        }

        var ratingDetailDto = new RatingDetailDto
        {
            Id = rating.Id,
            Comment = rating.Comment ?? "",
            RatingValue = rating.RatingValue,
            ProductId = rating.ProductId,
            ProductVariantId = rating.ProductVariantId,
            ProductVariantName = GetProductVariantName(rating.ProductVariant),
            CustomerId = rating.CustomerId,
            UserName = rating.Customer.UserName ?? "Anonymous User",
            FullName = rating.Customer.FullName ?? "Anonymous User",
            UserAvatarUrl = rating.Customer.ImageUrl,
            LikesCount = rating.Likes.Count(x => x.Liked),
            ReportsCount = rating.ReportsCount,
            Response = rating.Response,
            Status = rating.Status,
            ImageUrls = rating.Images?.Select(i => i.ImageUrl).ToList(),
            CreationTime = rating.CreationTime,
            CreatorId = rating.CreatorId,
            LastModificationTime = rating.LastModificationTime,
            LastModifierId = rating.LastModifierId,
            
            // AI Moderation properties
            AiModerationScore = rating.AiModerationScore,
            AiModerationCategory = rating.AiModerationCategory,
            AiModerationExplanation = rating.AiModerationExplanation,
            AiModerationDate = rating.AiModerationDate,
            IsAiModerated = rating.IsAiModerated,
            
            // Product information
            Product = rating.ProductVariant?.Product != null
                ? new ProductDetailInfoDto
                {
                    Id = rating.ProductVariant.Product.Id,
                    Name = rating.ProductVariant.Product.Name,
                    ThumbnailImageUrl = MappingHelpers.GetProductThumbnailImageUrl(rating.ProductVariant.Product),                    AverageRating = rating.ProductVariant.Product.Ratings
                        .Where(x => x.Status == RatingStatus.Posted)
                        .Any() ? rating.ProductVariant.Product.Ratings
                            .Where(x => x.Status == RatingStatus.Posted)
                            .Average(x => (decimal)x.RatingValue) : 0,
                    TotalRatingsCount = rating.ProductVariant.Product.Ratings
                        .Count(x => x.Status == RatingStatus.Posted)
                }
                : null,
                
            // Likes details
            Likes = rating.Likes?.Select(l => new RatingLikeDto
            {
                Id = l.Id,
                CustomerId = l.CustomerId,
                CustomerName = l.Customer?.FullName ?? "Anonymous User",
                Liked = l.Liked,
                CreationTime = l.CreationTime
            }).ToList(),
              // Reports details  
            Reports = rating.Reports?.Select(r => new RatingReportDto
            {
                Id = r.Id,
                CustomerId = r.CustomerId,
                CustomerName = "Anonymous User", // Customer navigation not available in RatingReport
                Reason = r.Reason,
                DetailedReason = r.DetailedReason,
                CreationTime = r.CreationTime
            }).ToList()
        };

        return Result.Success(ratingDetailDto);
    }

    private static string GetProductVariantName(ProductVariant? variant)
    {
        if (variant?.ProductVariantOptionValues == null || variant.ProductVariantOptionValues.Count == 0)
        {
            return variant?.VariantName ?? "Standard";
        }

        var optionValues = variant.ProductVariantOptionValues
            .Where(x => x.OptionValue != null)
            .Select(x => x.OptionValue!.Value)
            .ToList();

        return optionValues.Count > 0 ? string.Join(", ", optionValues) : "Standard";
    }
}
