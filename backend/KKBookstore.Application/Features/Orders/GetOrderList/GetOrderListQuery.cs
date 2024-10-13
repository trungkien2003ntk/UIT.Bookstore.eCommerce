using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Extensions;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KKBookstore.Application.Features.Orders.GetOrderList;

public record GetOrderListQuery : IRequest<Result<PaginatedResult<OrderGeneralInformation>>>
{
    public string SortBy { get; init; } = "CreatedWhen";
    public string SortDirection { get; init; } = "desc";
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 12;
    public List<string> OrderStatuses { get; init; } = [];
    public string Search { get; init; } = string.Empty;
}

public class GetOrderListHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetOrderListQuery, Result<PaginatedResult<OrderGeneralInformation>>>
{
    public async Task<Result<PaginatedResult<OrderGeneralInformation>>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Orders
            .Include(o => o.PaymentMethod)
            .Include(o => o.DeliveryMethod)
            .Include(o => o.ShippingDiscountVoucher)
            .Include(o => o.PriceDiscountVoucher)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Sku)
                    .ThenInclude(s => s.Product)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Sku)
                    .ThenInclude(s => s.SkuOptionValues)
                        .ThenInclude(sov => sov.Option)
                            .ThenInclude(o => o.OptionValues)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Sku)
                    .ThenInclude(s => s.SkuOptionValues)
                            .ThenInclude(o => o.OptionValue)
            .AsSplitQuery()
            .AsNoTracking();

        query = ApplyOrderStatusFilter(query, request);

        var sortProperty = request.SortBy;
        var validSortProperties = new List<string> { nameof(Order.CreatedWhen), nameof(Order.Id), nameof(Order.Status) };
        PaginatedResult<Order> paginatedOrders;

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
            return Result.Failure<PaginatedResult<OrderGeneralInformation>>(Error.InvalidSortProperty(sortProperty, string.Join(',', validSortProperties)));
        }

        var result = mapper.Map<PaginatedResult<OrderGeneralInformation>>(paginatedOrders);

        return Result.Success(result);
    }

    private static IQueryable<Order> ApplyOrderStatusFilter(IQueryable<Order> query, GetOrderListQuery request)
    {
        if (request.OrderStatuses.Any())
        {
            var orderStatusEnums = request.OrderStatuses
                .Select(status => Enum.Parse<OrderStatus>(status))
                .ToList();

            query = query.Where(o => orderStatusEnums.Contains(o.Status));
        }

        return query;
    }
}
