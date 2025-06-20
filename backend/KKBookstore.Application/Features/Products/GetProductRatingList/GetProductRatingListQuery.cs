using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Features.Products.Models;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Products.GetProductRatingList;

public record GetProductRatingListQuery : IRequest<Result<ProductRatingSummary>>, IPaginatedQuery
{
    public int ProductId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public List<string> Statuses { get; init; } = [];
}

public class GetProductRatingListQueryHandler(
        IApplicationDbContext dbContext
    ) : IRequestHandler<GetProductRatingListQuery, Result<ProductRatingSummary>>
{
    public async Task<Result<ProductRatingSummary>> Handle(GetProductRatingListQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Ratings
            .Include(r => r.ProductVariant)
                .ThenInclude(s => s.ProductVariantOptionValues)!
                    .ThenInclude(sov => sov.OptionValue)
            .Include(r => r.Customer)
            .Include(r => r.Likes)
            .Include(r => r.Images)
            .Where(x =>
                x.ProductVariant.ProductId == request.ProductId
                && (x.Status == RatingStatus.Posted || x.Status == RatingStatus.PendingReview)
            )
            .AsQueryable();

        var queryResult = ApplyStatusFilter(query, request);

        if (queryResult.IsFailure)
        {
            return Result.Failure<ProductRatingSummary>(queryResult.Error);
        }

        query = queryResult.Value;

        var allRatings = await query.ToListAsync(cancellationToken);

        PagedResult<Rating> paginatedRatings;

        paginatedRatings = await query.SortAndPaginateAsync(
            "CreationTime",
            "desc",
            ["CreationTime"],
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        if (paginatedRatings.Items.Count == 0)
        {
            return Result.Failure<ProductRatingSummary>(ProductErrors.RatingNotFound);
        }

        var result = MapToProductRatingSummary(paginatedRatings, allRatings);

        return Result.Success(result);
    }

    private Result<IQueryable<Rating>> ApplyStatusFilter(IQueryable<Rating> query, GetProductRatingListQuery request)
    {
        // check if the statuses in the request are valid
        if (request.Statuses.Count == 0)
        {
            return Result.Success(query);
        }

        if (request.Statuses.Exists(x => !Enum.TryParse<RatingStatus>(x, out _)))
        {
            return Result.Failure<IQueryable<Rating>>(
                ProductErrors.InvalidAttributeValue(
                    nameof(Rating.Status),
                    Enum.GetNames(typeof(RatingStatus))
                ));
        }

        var statuses = request.Statuses.Select(x => Enum.Parse<RatingStatus>(x)).ToList();

        return Result.Success(query.Where(x => statuses.Contains(x.Status)));
    }

    public ProductRatingSummary MapToProductRatingSummary(PagedResult<Rating> pagedRatings, List<Rating> allRatings)
    {
        var items = pagedRatings.Items.Select(r => new ProductRatingDto
        {
            Id = r.Id,
            Comment = r.Comment ?? "",
            RatingValue = r.RatingValue,
            CustomerId = r.CustomerId,
            UserName = r.Customer?.UserName ?? "Anonymous User",
            FullName = r.Customer?.FullName ?? "Anonymous User",
            UserAvatarUrl = r.Customer?.ImageUrl,
            ProductVariantName = r.ProductVariant.VariantName,
            LikesCount = r.Likes.Count(x => x.Liked),
            ReportsCount = r.ReportsCount,
            Response = r.Response,
            Status = r.Status,
            ImageUrls = r.Images?.Select(i => i.ImageUrl).ToList(),
            CreationTime = r.CreationTime,
            CreatorId = r.CreatorId,
            LastModificationTime = r.LastModificationTime,
            LastModifierId = r.LastModifierId,
        }).ToList();

        return new ProductRatingSummary
        {
            Ratings = new PagedResult<ProductRatingDto>(
                items,
                pagedRatings.TotalCount,
                pagedRatings.PageSize,
                pagedRatings.PageNumber
            ),
            AverageRating = allRatings.Count != 0
                ? allRatings.Average(r => (decimal)r.RatingValue)
                : 0m,

            TotalApprovedRating = allRatings.Count(r => r.Status == RatingStatus.Posted),
            TotalRatingWithComment = allRatings.Count(r => !string.IsNullOrEmpty(r.Comment)),
            Total5StarRating = allRatings.Count(r => r.RatingValue == 5),
            Total4StarRating = allRatings.Count(r => r.RatingValue == 4),
            Total3StarRating = allRatings.Count(r => r.RatingValue == 3),
            Total2StarRating = allRatings.Count(r => r.RatingValue == 2),
            Total1StarRating = allRatings.Count(r => r.RatingValue == 1),
            TotalRating = allRatings.Count,
            TotalRatingWithImage = allRatings.Count(r => r.Images != null && r.Images.Count != 0)
        };
    }
}