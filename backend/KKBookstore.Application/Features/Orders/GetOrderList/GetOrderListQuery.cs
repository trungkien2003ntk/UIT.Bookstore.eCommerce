﻿using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Application.Extensions;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Orders;
using KKBookstore.Domain.Shared.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KKBookstore.Application.Features.Orders.GetOrderList;

public record GetOrderListQuery : IRequest<Result<PagedResult<OrderGeneralInformation>>>
{
    public string SortBy { get; init; } = "CreationTime";
    public string SortDirection { get; init; } = "desc";
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 12;
    public List<string> OrderStatuses { get; init; } = [];
    public string SearchQuery { get; init; } = string.Empty;
}

public class GetOrderListHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetOrderListQuery, Result<PagedResult<OrderGeneralInformation>>>
{
    public async Task<Result<PagedResult<OrderGeneralInformation>>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Orders
            .Include(o => o.PaymentMethod)
            .Include(o => o.DeliveryMethod)
            .Include(o => o.ShippingDiscountVoucher)
            .Include(o => o.PriceDiscountVoucher)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(s => s.Product)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(s => s.ProductVariantOptionValues)
                        .ThenInclude(sov => sov.Option)
                            .ThenInclude(o => o.OptionValues)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(s => s.ProductVariantOptionValues)
                            .ThenInclude(o => o.OptionValue)
            .AsSplitQuery()
            .AsNoTracking();

        query = ApplyOrderStatusFilter(query, request);

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
