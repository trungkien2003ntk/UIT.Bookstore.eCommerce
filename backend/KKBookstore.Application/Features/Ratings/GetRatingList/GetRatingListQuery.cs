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
    public List<string> Statuses { get; init; } = [];
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
            .AsQueryable();

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
                r.Comment!.ToLower().Contains(searchTerm) ||
                r.Customer.UserName!.ToLower().Contains(searchTerm) ||
                r.Customer.FullName!.ToLower().Contains(searchTerm));
        }

        var allowedSortFields = new[] { "CreationTime", "RatingValue", "LikesCount" };
        
        var paginatedRatings = await query.SortAndPaginateAsync(
            request.SortBy,
            request.SortDirection,
            allowedSortFields.ToList(),
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        if (paginatedRatings.Items.Count == 0)
        {
            return Result.Success(new PagedResult<RatingDto>(
                [],
                0,
                request.PageSize,
                request.PageNumber
            ));
        }

        var result = MapToRatingDtoResult(paginatedRatings);
        return Result.Success(result);
    }

    private static IQueryable<Rating> ApplyFilters(IQueryable<Rating> query, GetRatingListQuery request)
    {
        if (request.ProductId.HasValue)
        {
            query = query.Where(r => r.ProductId == request.ProductId.Value);
        }

        if (request.CustomerId.HasValue)
        {
            query = query.Where(r => r.CustomerId == request.CustomerId.Value);
        }

        if (request.RatingValue.HasValue)
        {
            query = query.Where(r => r.RatingValue == request.RatingValue.Value);
        }

        return query;
    }

    private static Result<IQueryable<Rating>> ApplyStatusFilter(IQueryable<Rating> query, GetRatingListQuery request)
    {
        if (request.Statuses.Count == 0)
        {
            // Default to show only Posted and PendingReview ratings
            return Result.Success(query.Where(x => x.Status == RatingStatus.Posted || x.Status == RatingStatus.PendingReview));
        }

        // Check if the statuses in the request are valid
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
            LastModifierId = r.LastModifierId
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
