using AutoMapper;
using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KKBookstore.Features.Orders.GetOrderList;

public record GetOrderListQuery : IRequest<Result<PagedResult<OrderGeneralInformation>>>
{
    public string SortBy { get; init; } = "CreationTime";
    public string SortDirection { get; init; } = "desc";
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 12;
    public List<string> OrderStatuses { get; init; } = [];
    public string SearchQuery { get; init; } = string.Empty;
    
    // Date Range Filtering
    public DateTimeOffset? FromDate { get; init; }
    public DateTimeOffset? ToDate { get; init; }
    
    // Customer Filtering
    public int? CustomerId { get; init; }
    
    // Price Range Filtering
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
}

public class GetOrderListHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetOrderListQuery, Result<PagedResult<OrderGeneralInformation>>>
{    public async Task<Result<PagedResult<OrderGeneralInformation>>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Orders
            .Include(o => o.PaymentMethod)
            .Include(o => o.DeliveryMethod)
            .Include(o => o.ShippingDiscountVoucher)
            .Include(o => o.PriceDiscountVoucher)
            .Include(o => o.Customer)
            .Include(o => o.ShippingAddress)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(s => s.Product)
                        .ThenInclude(p => p.ProductImages)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(s => s.ProductVariantOptionValues)!
                        .ThenInclude(sov => sov.Option)
                            .ThenInclude(o => o.OptionValues)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(s => s.ProductVariantOptionValues)!
                            .ThenInclude(o => o.OptionValue)
            .AsSplitQuery()
            .AsNoTracking();

        // Apply all filters
        query = ApplyOrderStatusFilter(query, request);
        query = ApplySearchFilter(query, request);
        query = ApplyDateRangeFilter(query, request);
        query = ApplyCustomerFilter(query, request);
        query = ApplyPriceRangeFilter(query, request);

        var sortProperty = request.SortBy;
        var validSortProperties = new List<string> { nameof(Order.CreationTime), nameof(Order.Id), nameof(Order.Status) };
        PagedResult<Order> paginatedOrders;

        try
        {
            paginatedOrders = await query.SortAndPaginateAsync(
                sortProperty,
                request.SortDirection,
                validSortProperties,
                request.PageNumber,
                request.PageSize,
                cancellationToken);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Failure<PagedResult<OrderGeneralInformation>>(Error.InvalidSortProperty(sortProperty, string.Join(',', validSortProperties)));
        }

        var result = mapper.Map<PagedResult<OrderGeneralInformation>>(paginatedOrders);

        return Result.Success(result);
    }    private static IQueryable<Order> ApplyOrderStatusFilter(IQueryable<Order> query, GetOrderListQuery request)
    {
        if (request.OrderStatuses.Any())
        {
            var orderStatusEnums = request.OrderStatuses
                .Select(status => Enum.Parse<OrderStatus>(status))
                .ToList();

            query = query.Where(o => orderStatusEnums.Contains(o.Status));
        }

        return query;
    }    private static IQueryable<Order> ApplySearchFilter(IQueryable<Order> query, GetOrderListQuery request)
    {
        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchQuery = request.SearchQuery.Trim().ToLower();
            
            query = query.Where(o =>
                // Search by order number
                o.OrderNumber.ToLower().Contains(searchQuery) ||
                // Search by customer name
                (o.Customer != null && (o.Customer.FirstName + " " + o.Customer.LastName).ToLower().Contains(searchQuery)) ||
                // Search by customer email
                (o.Customer != null && o.Customer.Email != null && o.Customer.Email.ToLower().Contains(searchQuery))
            );
        }

        return query;
    }

    private static IQueryable<Order> ApplyDateRangeFilter(IQueryable<Order> query, GetOrderListQuery request)
    {
        if (request.FromDate.HasValue)
        {
            query = query.Where(o => o.CreationTime >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            // Add one day to include the entire end date
            var endDate = request.ToDate.Value.Date.AddDays(1);
            query = query.Where(o => o.CreationTime < endDate);
        }

        return query;
    }

    private static IQueryable<Order> ApplyCustomerFilter(IQueryable<Order> query, GetOrderListQuery request)
    {
        if (request.CustomerId.HasValue)
        {
            query = query.Where(o => o.CustomerId == request.CustomerId.Value);
        }

        return query;
    }

    private static IQueryable<Order> ApplyPriceRangeFilter(IQueryable<Order> query, GetOrderListQuery request)
    {
        if (request.MinPrice.HasValue)
        {
            query = query.Where(o => o.CalculateTotal() >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(o => o.CalculateTotal() <= request.MaxPrice.Value);
        }

        return query;
    }
}
