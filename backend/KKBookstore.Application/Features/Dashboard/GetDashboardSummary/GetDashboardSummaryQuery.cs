using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Dashboard.GetDashboardSummary;

public record GetDashboardSummaryQuery : IRequest<Result<Models.DashboardSummaryDto>>
{
    public DateTimeOffset? FromDate { get; init; }
    public DateTimeOffset? ToDate { get; init; }
    public string Period { get; init; } = "month"; // week, month, year
    public int? BranchId { get; init; }
    public List<int> ProductTypeIds { get; init; } = [];
    public int TopProductsLimit { get; init; } = 5;
}
