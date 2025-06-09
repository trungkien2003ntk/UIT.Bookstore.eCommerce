using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Products.Models;
using KKBookstore.Mappings.Helpers;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Products.LikeProductRating;

public record LikeProductRatingCommand(int RatingId, int CustomerId) : IRequest<Result<ProductRatingDto>>
{
}

public class LikeProductRatingCommandHandler : IRequestHandler<LikeProductRatingCommand, Result<ProductRatingDto>>
{
    private readonly IApplicationDbContext _dbContext;
    public LikeProductRatingCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<ProductRatingDto>> Handle(LikeProductRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = await _dbContext.Ratings
            .Include(r => r.ProductVariant)
                .ThenInclude(v => v.ProductVariantOptionValues)!
                    .ThenInclude(x => x.OptionValue)
            .Include(r => r.Customer)
            .Include(r => r.Likes)
            .FirstOrDefaultAsync(r => r.Id == request.RatingId, cancellationToken);
        if (rating == null)
        {
            return Result.Failure<ProductRatingDto>(ProductErrors.RatingNotFound);
        }

        var existingLike = await _dbContext.RatingLikes
            .FirstOrDefaultAsync(l => l.RatingId == request.RatingId && l.CustomerId == request.CustomerId, cancellationToken);

        if (existingLike != null)
        {
            // If the like already exists, remove it
            _dbContext.RatingLikes.Remove(existingLike);
        }
        else
        {
            // If the like does not exist, add it
            var like = new RatingLike(request.RatingId, request.CustomerId, true, DateTimeOffset.Now);
            await _dbContext.RatingLikes.AddAsync(like, cancellationToken);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);

        var ratingDto = new ProductRatingDto
        {
            Id = rating.Id,
            Comment = rating.Comment!,
            RatingValue = rating.RatingValue,
            UserName = rating.Customer!.UserName ?? "Anonymous User",
            FullName = rating.Customer.FullName ?? "Anonymous User",
            UserAvatarUrl = rating.Customer.ImageUrl,
            ProductVariantName = MappingHelpers.GetProductVariantOptionValuesString(rating.ProductVariant),
            LikesCount = rating.Likes.Count,
            ReportsCount = rating.ReportsCount,
            Response = rating.Response,
            Status = rating.Status,
            CreationTime = rating.CreationTime,
            ImageUrls = rating.Images?.Select(i => i.ImageUrl).ToList() ?? [],
        };

        return Result.Success(ratingDto);
    }
}
