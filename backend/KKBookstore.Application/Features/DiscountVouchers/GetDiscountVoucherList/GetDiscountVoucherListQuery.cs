using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Features.DiscountVouchers.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using KKBookstore.ShoppingCarts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.DiscountVouchers.GetDiscountVoucherList;

public record GetDiscountVoucherListQuery()
    : IRequest<Result<PagedResult<DiscountVoucherDto>>>, IPaginatedQuery, ISortableQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string SortBy { get; init; } = "CreationTime";
    public string SortDirection { get; init; } = "desc";

    // Filters
    public string? SearchQuery { get; init; }
    public DiscountStatus? Status { get; init; }
    public DiscountVoucherType? VoucherType { get; init; }
    public DiscountValueType? ValueType { get; init; }
    public int? ApplyToProductTypeId { get; init; }
    public DateTimeOffset? StartDate { get; init; }
    public DateTimeOffset? EndDate { get; init; }
    public decimal? MinValue { get; init; }
    public decimal? MaxValue { get; init; }

    // Cart integration - optional parameters for voucher applicability check
    public int? UserId { get; init; }
    public List<int> SelectedCartItemIds { get; init; } = [];
}

public class GetDiscountVoucherListQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetDiscountVoucherListQuery, Result<PagedResult<DiscountVoucherDto>>>
{
    public async Task<Result<PagedResult<DiscountVoucherDto>>> Handle(GetDiscountVoucherListQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.DiscountVouchers
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

        // Calculate cart total if cart integration parameters are provided
        decimal? cartTotal = null;
        if (request.UserId.HasValue && request.SelectedCartItemIds.Any())
        {
            cartTotal = await CalculateSelectedCartItemsTotal(request.UserId.Value, request.SelectedCartItemIds, cancellationToken);
        }

        var distinctProductTypeIds = request.SelectedCartItemIds.Any()
            ? await dbContext.ShoppingCartItems
                .Where(sci => sci.CustomerId == request.UserId && request.SelectedCartItemIds.Contains(sci.Id))
                .Select(sci => sci.ProductVariant.Product.ProductTypeId)
                .Distinct()
                .ToListAsync(cancellationToken)
            : [];

        var result = MapToDiscountVoucherDtoResult(paginatedVouchers!, request.UserId, cartTotal, distinctProductTypeIds);
        return Result.Success(result);
    }
    private static IQueryable<DiscountVoucher> ApplyFilters(IQueryable<DiscountVoucher> query, GetDiscountVoucherListQuery request)
    {
        query = query
            .WhereIf(request.Status.HasValue, dv => dv.Status == request.Status!.Value)
            .WhereIf(request.VoucherType.HasValue, dv => dv.VoucherType == request.VoucherType!.Value)
            .WhereIf(request.ValueType.HasValue, dv => dv.ValueType == request.ValueType!.Value)
            .WhereIf(request.ApplyToProductTypeId.HasValue, dv =>
                dv.ApplyToProductTypeIds != null &&
                dv.ApplyToProductTypeIds.Contains(request.ApplyToProductTypeId!.Value.ToString()))
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

    private async Task<decimal> CalculateSelectedCartItemsTotal(int userId, List<int> selectedItemIds, CancellationToken cancellationToken)
    {
        var selectedCartItems = await dbContext.ShoppingCartItems
            .Where(sci => sci.CustomerId == userId && selectedItemIds.Contains(sci.Id))
            .Include(sci => sci.ProductVariant)
                .ThenInclude(pv => pv.Inventories)
            .ToListAsync(cancellationToken);

        var createShoppingCartResult = ShoppingCart.Create(userId, selectedCartItems);
        if (createShoppingCartResult.IsFailure)
        {
            return 0m;
        }

        var shoppingCart = createShoppingCartResult.Value;
        shoppingCart.SelectItems(selectedItemIds);
        return shoppingCart.TotalUnitPrice;
    }

    private static PagedResult<DiscountVoucherDto> MapToDiscountVoucherDtoResult(
        PagedResult<DiscountVoucher> paginatedVouchers,
        int? userId = null,
        decimal? cartTotal = null,
        List<int>? distinctProductTypeIds = null)
    {
        var items = paginatedVouchers.Items.Select(dv =>
        {
            // Determine if voucher can be applied to cart (if cart integration params provided)
            bool canApply = false;
            if (userId.HasValue && cartTotal.HasValue)
            {
                canApply = dv.Status == DiscountStatus.Active &&
                          dv.StartTime <= DateTimeOffset.Now &&
                          dv.EndTime >= DateTimeOffset.Now &&
                          dv.IsApplicable(cartTotal.Value, userId.Value, distinctProductTypeIds!);
            }

            return new DiscountVoucherDto
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
                ApplyToProductTypeIds = dv.ApplyToProductTypeIds,
                ApplyToProductTypeIdsList = !string.IsNullOrEmpty(dv.ApplyToProductTypeIds)
                    ? dv.ApplyToProductTypeIds.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
                    : new List<int>(),
                ApplyToProductTypes = new List<ApplyToProductTypeDto>(), // Empty for list query - populate in detail query
                CustomerTypeIds = dv.CustomerTypes.Select(vct => vct.CustomerTypeId).ToList(),
                CustomerTypeNames = dv.CustomerTypes.Select(vct => vct.CustomerType.Name).ToList(),
                UsageCount = dv.VoucherUsages.Count,
                UsedPercentage = dv.UsageLimitOverall == 0 ? 0 : (decimal)dv.VoucherUsages.Count / dv.UsageLimitOverall,
                CustomerTypes = dv.CustomerTypes.Select(vct => new CustomerTypeDto
                {
                    Id = vct.CustomerType.Id,
                    Name = vct.CustomerType.Name
                }).ToList(),
                CreationTime = dv.CreationTime,
                CreatorId = dv.CreatorId,
                LastModificationTime = dv.LastModificationTime,
                LastModifierId = dv.LastModifierId,
                CanApply = canApply
            };
        }).ToList();

        return new PagedResult<DiscountVoucherDto>(
            items,
            paginatedVouchers.TotalCount,
            paginatedVouchers.PageSize,
            paginatedVouchers.PageNumber
        );
    }
}
