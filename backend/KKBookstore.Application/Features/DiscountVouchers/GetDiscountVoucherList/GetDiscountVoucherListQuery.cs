using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Features.DiscountVouchers.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.DiscountVouchers.GetDiscountVoucherList;

public record GetDiscountVoucherListQuery()
    : IRequest<Result<PagedResult<DiscountVoucherDto>>>, IPaginatedQuery, ISortableQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string SortBy { get; init; } = "CreationTime";
    public string SortDirection { get; init; } = "desc";    // Filters
    public string? SearchQuery { get; init; }
    public DiscountStatus? Status { get; init; }
    public DiscountVoucherType? VoucherType { get; init; }
    public DiscountValueType? ValueType { get; init; }
    public int? ApplyToProductTypeId { get; init; }
    public DateTimeOffset? StartDate { get; init; }
    public DateTimeOffset? EndDate { get; init; }
    public decimal? MinValue { get; init; }
    public decimal? MaxValue { get; init; }
}

public class GetDiscountVoucherListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetDiscountVoucherListQuery, Result<PagedResult<DiscountVoucherDto>>>
{
    public async Task<Result<PagedResult<DiscountVoucherDto>>> Handle(GetDiscountVoucherListQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.DiscountVouchers
            .Include(dv => dv.ApplyToProductType)
            .Include(dv => dv.VoucherUsages)
            .Include(dv => dv.CustomerTypes)
                .ThenInclude(vct => vct.CustomerType)
            .AsQueryable()
            .AsSplitQuery();

        // Apply filters
        query = ApplyFilters(query, request);

        // Apply search filter
        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchTerm = request.SearchQuery.ToLower();
            query = query.Where(dv =>
                EF.Functions.Like(dv.Name, $"%{request.SearchQuery}%") ||
                EF.Functions.Like(dv.Code, $"%{request.SearchQuery}%") ||
                EF.Functions.Like(dv.Description, $"%{request.SearchQuery}%"));
        }

        var allowedSortFields = new[] { "CreationTime", "Name", "Code", "StartTime", "EndTime", "Value", "Status" };
        PagedResult<DiscountVoucher>? paginatedVouchers = null;

        try
        {
            paginatedVouchers = await query.SortAndPaginateAsync(
                request.SortBy,
                request.SortDirection,
                allowedSortFields.ToList(),
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
        catch (ArgumentException)
        {
            return Result.Failure<PagedResult<DiscountVoucherDto>>(
                Error.InvalidSortProperty(request.SortBy, string.Join(",", allowedSortFields)));
        }

        if (paginatedVouchers!.Items.Count == 0)
        {
            return Result.Success(new PagedResult<DiscountVoucherDto>(
                [],
                0,
                request.PageSize,
                request.PageNumber
            ));
        }

        var result = MapToDiscountVoucherDtoResult(paginatedVouchers!);
        return Result.Success(result);
    }    private static IQueryable<DiscountVoucher> ApplyFilters(IQueryable<DiscountVoucher> query, GetDiscountVoucherListQuery request)
    {
        query = query
            .WhereIf(request.Status.HasValue, dv => dv.Status == request.Status!.Value)
            .WhereIf(request.VoucherType.HasValue, dv => dv.VoucherType == request.VoucherType!.Value)
            .WhereIf(request.ValueType.HasValue, dv => dv.ValueType == request.ValueType!.Value)
            .WhereIf(request.ApplyToProductTypeId.HasValue, dv => dv.ApplyToProductTypeId == request.ApplyToProductTypeId!.Value)
            .WhereIf(request.MinValue.HasValue, dv => dv.Value >= request.MinValue!.Value)
            .WhereIf(request.MaxValue.HasValue, dv => dv.Value <= request.MaxValue!.Value);

        // Apply duration overlap filter: voucher overlaps with the specified date range
        if (request.StartDate.HasValue && request.EndDate.HasValue)
        {
            query = query.Where(dv => 
                dv.StartTime <= request.EndDate!.Value && 
                dv.EndTime >= request.StartDate!.Value);
        }
        else if (request.StartDate.HasValue)
        {
            query = query.Where(dv => dv.EndTime >= request.StartDate!.Value);
        }
        else if (request.EndDate.HasValue)
        {
            query = query.Where(dv => dv.StartTime <= request.EndDate!.Value);
        }

        return query;
    }

    private static PagedResult<DiscountVoucherDto> MapToDiscountVoucherDtoResult(PagedResult<DiscountVoucher> paginatedVouchers)
    {
        var items = paginatedVouchers.Items.Select(dv => new DiscountVoucherDto
        {
            Id = dv.Id,
            Name = dv.Name,
            Code = dv.Code,
            Description = dv.Description,
            ValueType = dv.ValueType,
            VoucherType = dv.VoucherType,
            Status = dv.Status,
            Value = dv.Value,
            MaximumDiscountValue = dv.MaximumDiscountValue,
            MinimumSpend = dv.MinimumSpend,
            UsageLimitPerUser = dv.UsageLimitPerUser,
            UsageLimitOverall = dv.UsageLimitOverall,
            StartTime = dv.StartTime,
            EndTime = dv.EndTime,
            ApplyToProductTypeId = dv.ApplyToProductTypeId,
            ApplyToProductTypeName = dv.ApplyToProductType?.DisplayName,
            CustomerTypeIds = dv.CustomerTypes.Select(vct => vct.CustomerTypeId).ToList(),
            CustomerTypeNames = dv.CustomerTypes.Select(vct => vct.CustomerType.Name).ToList(),
            UsageCount = dv.VoucherUsages.Count,
            UsedPercentage = dv.UsageLimitOverall == 0 ? 0 : (decimal)dv.VoucherUsages.Count / dv.UsageLimitOverall,
            CreationTime = dv.CreationTime,
            CreatorId = dv.CreatorId,
            LastModificationTime = dv.LastModificationTime,
            LastModifierId = dv.LastModifierId
        }).ToList();

        return new PagedResult<DiscountVoucherDto>(
            items,
            paginatedVouchers.TotalCount,
            paginatedVouchers.PageSize,
            paginatedVouchers.PageNumber
        );
    }
}
