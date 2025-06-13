using KKBookstore.Banners;
using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Features.Banners.Models;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Banners.GetBannerList;

public record GetBannerListQuery()
    : IRequest<Result<PagedResult<BannerDto>>>, IPaginatedQuery, ISortableQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string SortBy { get; init; } = "CreationTime";
    public string SortDirection { get; init; } = "desc";

    // Filters
    public string? SearchQuery { get; init; }
    public bool? IsActive { get; init; }
    public int? ProductTypeId { get; init; }
}

public class GetBannerListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetBannerListQuery, Result<PagedResult<BannerDto>>>
{
    public async Task<Result<PagedResult<BannerDto>>> Handle(GetBannerListQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Banners
            .Include(b => b.ProductType)
            .AsQueryable();

        // Apply filters
        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchTerm = request.SearchQuery.ToLower();
            query = query.Where(b =>
                b.Title.ToLower().Contains(searchTerm) ||
                (b.TargetUrl != null && b.TargetUrl.ToLower().Contains(searchTerm)) ||
                (b.ProductType != null && b.ProductType.DisplayName.ToLower().Contains(searchTerm)));
        }

        if (request.IsActive.HasValue)
        {
            query = query.Where(b => b.IsActive == request.IsActive.Value);
        }

        if (request.ProductTypeId.HasValue)
        {
            query = query.Where(b => b.ProductTypeId == request.ProductTypeId.Value);
        }        // Apply sorting and pagination
        var validSortProperties = new List<string>
        {
            nameof(Banner.Id),
            nameof(Banner.Title),
            nameof(Banner.IsActive),
            nameof(Banner.CreationTime),
            nameof(Banner.ProductTypeId)
        };

        var sortAndPagingResult = await query.SortAndPaginateWithResultAsync(
            request.SortBy,
            request.SortDirection,
            validSortProperties,
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        if (sortAndPagingResult.IsFailure)
        {
            return Result.Failure<PagedResult<BannerDto>>(sortAndPagingResult.Error);
        }

        var paginatedBanners = sortAndPagingResult.Value;

        if (paginatedBanners.Items.Count == 0)
        {
            return Result.Failure<PagedResult<BannerDto>>(
                Error.NotFound("Banner.NotFound", "No banners were found."));
        }

        var mappedPaginatedBanners = MapToBannerDtoResult(paginatedBanners);

        return Result.Success(mappedPaginatedBanners);
    }

    private PagedResult<BannerDto> MapToBannerDtoResult(PagedResult<Banner> paginatedBanners)
    {
        return new PagedResult<BannerDto>(
            paginatedBanners.Items.Select(b => new BannerDto
            {
                Id = b.Id,
                Title = b.Title,
                ImageUrl = b.ImageUrl,
                TargetUrl = b.TargetUrl,
                ProductTypeId = b.ProductTypeId,
                ProductTypeName = b.ProductType?.DisplayName,
                IsActive = b.IsActive,
                CreationTime = b.CreationTime,
                CreatorId = b.CreatorId,
                LastModificationTime = b.LastModificationTime,
                LastModifierId = b.LastModifierId
            }).ToList(),
            paginatedBanners.TotalCount,
            paginatedBanners.PageSize,
            paginatedBanners.PageNumber
        );
    }
}
