using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Dashboard.GetRevenueAnalytics;

public record GetRevenueAnalyticsQuery : IRequest<Result<Models.RevenueAnalyticsDto>>
{
    public DateTimeOffset? FromDate { get; init; }
    public DateTimeOffset? ToDate { get; init; }
    public string Period { get; init; } = "month"; // day, week, month, year
    public string GroupBy { get; init; } = "day"; // day, week, month
    public int? BranchId { get; init; }
    public List<int> ProductTypeIds { get; init; } = [];
}
