using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Products.Models;
using KKBookstore.Mappings.Helpers;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Products.ReportProductRating;

public record ReportProductRatingCommand(int RatingId, int CustomerId, string Reason) : IRequest<Result<ProductRatingDto>>
{
}

public class ReportProductRatingCommandHandler : IRequestHandler<ReportProductRatingCommand, Result<ProductRatingDto>>
{
    private readonly IApplicationDbContext _dbContext;
    public ReportProductRatingCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result<ProductRatingDto>> Handle(ReportProductRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = await _dbContext.Ratings
            .Include(r => r.ProductVariant)
                .ThenInclude(v => v.ProductVariantOptionValues)!
                    .ThenInclude(x => x.OptionValue)
            .Include(r => r.Customer)
            .Include(r => r.Reports)
            .Include(r => r.Likes)
            .FirstOrDefaultAsync(r => r.Id == request.RatingId, cancellationToken);
        if (rating == null)
        {
            return Result.Failure<ProductRatingDto>(ProductErrors.RatingNotFound);
        }

        var reportResult = rating.ReportedBy(rating.CustomerId, request.Reason);
        if (reportResult.IsFailure)
        {
            return Result.Failure<ProductRatingDto>(reportResult.Error);
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
            ReportsCount = rating.ReportsCount,
            LikesCount = rating.Likes.Count,
            ImageUrls = rating.Images?.Select(i => i.ImageUrl).ToList(),
            Status = rating.Status,
            Response = rating.Response,
            CreationTime = rating.CreationTime
        };

        return Result.Success(ratingDto);
    }
}
