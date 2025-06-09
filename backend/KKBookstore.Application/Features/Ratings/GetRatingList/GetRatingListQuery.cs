using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Features.Ratings.Models;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Ratings.GetRatingList;

public record GetRatingListQuery()
    : IRequest<Result<PagedResult<RatingDto>>>, IPaginatedQuery, ISortableQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string SortBy { get; init; } = "CreationTime";
    public string SortDirection { get; init; } = "desc";

    // Filters
    public int? ProductId { get; init; }
    public int? CustomerId { get; init; }
    public RatingStatus? Status { get; init; }
    public int? RatingValue { get; init; }
    public string? SearchQuery { get; init; }
}

public class GetRatingListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetRatingListQuery, Result<PagedResult<RatingDto>>>
{
    public async Task<Result<PagedResult<RatingDto>>> Handle(GetRatingListQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Ratings
            .Include(r => r.ProductVariant)
                .ThenInclude(v => v.ProductVariantOptionValues)!
                    .ThenInclude(x => x.OptionValue)
            .Include(r => r.Customer)
            .Include(r => r.Likes)
            .Include(r => r.Images)
            .Include(r => r.ProductVariant!)
                .ThenInclude(v => v.Product)
                    .ThenInclude(p => p.Ratings)
            .Include(r => r.ProductVariant!)
                .ThenInclude(v => v.Product)
                    .ThenInclude(p => p.ProductImages)
            .AsQueryable()
            .AsSplitQuery();

        // Apply filters
        query = ApplyFilters(query, request);

        // Apply status filter
        var statusFilterResult = ApplyStatusFilter(query, request);
        if (statusFilterResult.IsFailure)
        {
            return Result.Failure<PagedResult<RatingDto>>(statusFilterResult.Error);
        }
        query = statusFilterResult.Value;

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchTerm = request.SearchQuery.ToLower();
            query = query.Where(r =>
                EF.Functions.Like(r.ProductVariant.Product.Name, $"%{request.SearchQuery}%") ||
                r.ProductVariant.Product.Id.ToString().Contains(searchTerm));
        }

        var allowedSortFields = new[] { "CreationTime", "RatingValue", "LikesCount" };
        PagedResult<Rating>? paginatedRatings = null;
        try
        {
            paginatedRatings = await query.SortAndPaginateAsync(
            request.SortBy,
            request.SortDirection,
            allowedSortFields.ToList(),
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        }
        catch (ArgumentException ex)
        {
            return Result.Failure<PagedResult<RatingDto>>(Error.InvalidSortProperty(request.SortBy, string.Join(",", allowedSortFields)));
        }
        ;

        if (paginatedRatings!.Items.Count == 0)
        {
            return Result.Success(new PagedResult<RatingDto>(
                [],
                0,
                request.PageSize,
                request.PageNumber
            ));
        }

        var result = MapToRatingDtoResult(paginatedRatings!);
        return Result.Success(result);
    }

    private static IQueryable<Rating> ApplyFilters(IQueryable<Rating> query, GetRatingListQuery request)
    {
        query = query
            .WhereIf(request.ProductId.HasValue, r => r.ProductId == request.ProductId!.Value)
            .WhereIf(request.CustomerId.HasValue, r => r.CustomerId == request.CustomerId!.Value)
            .WhereIf(request.RatingValue.HasValue, r => r.RatingValue == request.RatingValue!.Value);

        return query;
    }

    private static Result<IQueryable<Rating>> ApplyStatusFilter(IQueryable<Rating> query, GetRatingListQuery request)
    {
        return Result.Success(query.WhereIf(request.Status.HasValue, x => x.Status == request.Status!.Value));
    }

    private static PagedResult<RatingDto> MapToRatingDtoResult(PagedResult<Rating> paginatedRatings)
    {
        var items = paginatedRatings.Items.Select(r => new RatingDto
        {
            Id = r.Id,
            Comment = r.Comment ?? "",
            RatingValue = r.RatingValue,
            ProductId = r.ProductId,
            ProductVariantId = r.ProductVariantId,
            ProductVariantName = GetProductVariantName(r.ProductVariant),
            CustomerId = r.CustomerId,
            UserName = r.Customer.UserName ?? "Anonymous User",
            FullName = r.Customer.FullName ?? "Anonymous User",
            UserAvatarUrl = r.Customer.ImageUrl,
            LikesCount = r.Likes.Count(x => x.Liked),
            ReportsCount = r.ReportsCount,
            Response = r.Response,
            Status = r.Status,
            ImageUrls = r.Images?.Select(i => i.ImageUrl).ToList(),
            CreationTime = r.CreationTime,
            CreatorId = r.CreatorId,
            LastModificationTime = r.LastModificationTime,
            LastModifierId = r.LastModifierId,
            Product = r.ProductVariant?.Product != null
                ? new ProductBasicInfoDto
                {
                    Id = r.ProductVariant.Product.Id,
                    Name = r.ProductVariant.Product.Name,
                    ThumbnailImageUrl = r.ProductVariant.Product.GetFirstThumbnailImageUrl(),
                    AverageRating = r.ProductVariant.Product.Ratings
                        .Where(x => x.Status == RatingStatus.Posted)
                        .Average(x => (decimal)x.RatingValue)
                }
                : null
        }).ToList();

        return new PagedResult<RatingDto>(
            items,
            paginatedRatings.TotalCount,
            paginatedRatings.PageSize,
            paginatedRatings.PageNumber
        );
    }

    private static string GetProductVariantName(ProductVariant variant)
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
