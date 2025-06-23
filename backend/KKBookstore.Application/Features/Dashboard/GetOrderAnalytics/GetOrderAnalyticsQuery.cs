using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Dashboard.GetOrderAnalytics;

public record GetOrderAnalyticsQuery : IRequest<Result<Models.OrderAnalyticsDto>>
{
    public DateTimeOffset? FromDate { get; init; }
    public DateTimeOffset? ToDate { get; init; }
    public string Period { get; init; } = "month"; // week, month, year
    public int? BranchId { get; init; }
    public List<string> OrderStatuses { get; init; } = [];
}
