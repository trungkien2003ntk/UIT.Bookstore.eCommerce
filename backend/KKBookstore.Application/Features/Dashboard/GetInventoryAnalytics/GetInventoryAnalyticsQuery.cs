using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Dashboard.GetInventoryAnalytics;

public record GetInventoryAnalyticsQuery : IRequest<Result<Models.InventoryAnalyticsDto>>
{
    public DateTimeOffset? FromDate { get; init; }
    public DateTimeOffset? ToDate { get; init; }
    public string Period { get; init; } = "month"; // week, month, year
    public int? BranchId { get; init; }
    public List<int> ProductTypeIds { get; init; } = [];
    public int LowStockThreshold { get; init; } = 10;
    public int LowStockProductsLimit { get; init; } = 20;
}
