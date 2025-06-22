using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Dashboard.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using KKBookstore.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Dashboard.GetDashboardSummary;

public class GetDashboardSummaryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetDashboardSummaryQuery, Result<DashboardSummaryDto>>
{
    public async Task<Result<DashboardSummaryDto>> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var dateFilter = GetDateFilter(request.Period, request.FromDate, request.ToDate);
            
            // Get total orders
            var ordersQuery = dbContext.Orders.AsQueryable();
            if (dateFilter.FromDate.HasValue)
                ordersQuery = ordersQuery.Where(o => o.OrderWhen >= dateFilter.FromDate.Value);
            if (dateFilter.ToDate.HasValue)
                ordersQuery = ordersQuery.Where(o => o.OrderWhen <= dateFilter.ToDate.Value);
            
            var totalOrders = await ordersQuery.CountAsync(cancellationToken);
            
            // Get total new users
            var usersQuery = dbContext.Users.AsQueryable();
            if (dateFilter.FromDate.HasValue)
                usersQuery = usersQuery.Where(u => u.CreationTime >= dateFilter.FromDate.Value);
            if (dateFilter.ToDate.HasValue)
                usersQuery = usersQuery.Where(u => u.CreationTime <= dateFilter.ToDate.Value);
            
            var totalNewUsers = await usersQuery.CountAsync(cancellationToken);
            
            // Get total revenue using Subtotal
            var totalRevenue = await ordersQuery
                .Where(o => o.Status == OrderStatus.Delivered)
                .SumAsync(o => o.Subtotal, cancellationToken);
            
            // Get top products (simplified)
            var topProducts = await dbContext.OrderLines
                .Include(ol => ol.ProductVariant)
                    .ThenInclude(pv => pv.Product)
                .Where(ol => dateFilter.FromDate == null || ol.Order.OrderWhen >= dateFilter.FromDate.Value)
                .Where(ol => dateFilter.ToDate == null || ol.Order.OrderWhen <= dateFilter.ToDate.Value)
                .GroupBy(ol => new { ol.ProductVariant.Product.Id, ol.ProductVariant.Product.Name })
                .Select(g => new TopProductDto
                {
                    ProductId = g.Key.Id,
                    ProductName = g.Key.Name,
                    TotalQuantitySold = g.Sum(ol => ol.Quantity),
                    TotalRevenue = g.Sum(ol => ol.Quantity * ol.UnitPrice)
                })
                .OrderByDescending(tp => tp.TotalQuantitySold)
                .Take(5)
                .ToListAsync(cancellationToken);
            
            // Get sales by product types (simplified)
            var salesByProductTypes = await dbContext.OrderLines
                .Include(ol => ol.ProductVariant)
                    .ThenInclude(pv => pv.Product)
                        .ThenInclude(p => p.ProductType)
                .Where(ol => dateFilter.FromDate == null || ol.Order.OrderWhen >= dateFilter.FromDate.Value)
                .Where(ol => dateFilter.ToDate == null || ol.Order.OrderWhen <= dateFilter.ToDate.Value)
                .GroupBy(ol => new { 
                    ol.ProductVariant.Product.ProductType.Id, 
                    ol.ProductVariant.Product.ProductType.DisplayName 
                })
                .Select(g => new SalesByProductTypeDto
                {
                    ProductTypeId = g.Key.Id,
                    ProductTypeName = g.Key.DisplayName,
                    TotalQuantitySold = g.Sum(ol => ol.Quantity),
                    TotalRevenue = g.Sum(ol => ol.Quantity * ol.UnitPrice)
                })
                .OrderByDescending(s => s.TotalRevenue)
                .ToListAsync(cancellationToken);

            var result = new DashboardSummaryDto
            {
                TotalOrders = totalOrders,
                TotalNewUsers = totalNewUsers,
                TotalRevenue = totalRevenue,
                TopProducts = topProducts,
                SalesByProductTypes = salesByProductTypes,
                TotalStockAdjustmentOrders = 0, // TODO: Implement when stock transactions are available
                AverageOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0,
                TotalProductsSold = topProducts.Sum(p => p.TotalQuantitySold)
            };

            return Result<DashboardSummaryDto>.Success(result);        }
        catch (Exception ex)
        {
            var error = Error.Failure("Dashboard.SummaryError", $"Error retrieving dashboard summary: {ex.Message}");
            return Result.Failure<DashboardSummaryDto>(error);
        }
    }

    private (DateTimeOffset? FromDate, DateTimeOffset? ToDate) GetDateFilter(string period, DateTimeOffset? fromDate, DateTimeOffset? toDate)
    {
        if (fromDate.HasValue && toDate.HasValue)
            return (fromDate, toDate);

        var now = DateTimeOffset.Now;
        return period?.ToLower() switch
        {
            "week" => (now.AddDays(-7), now),
            "month" => (now.AddMonths(-1), now),
            "year" => (now.AddYears(-1), now),
            _ => (fromDate, toDate)
        };
    }
}
