using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Dashboard.GetCustomerAnalytics;

public record GetCustomerAnalyticsQuery : IRequest<Result<Models.CustomerAnalyticsDto>>
{
    public DateTimeOffset? FromDate { get; init; }
    public DateTimeOffset? ToDate { get; init; }
    public string Period { get; init; } = "month"; // week, month, year
    public string GroupBy { get; init; } = "day"; // day, week, month
    public int? BranchId { get; init; }
}
