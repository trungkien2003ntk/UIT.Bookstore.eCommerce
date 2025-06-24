using KKBookstore.Features.Dashboard.Models;
using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Dashboard.GetProfitAnalytics;

public class GetProfitAnalyticsQuery : IRequest<Result<ProfitAnalyticsDto>>
{
    public string Period { get; set; } = "month";
    public DateTimeOffset? FromDate { get; set; }
    public DateTimeOffset? ToDate { get; set; }
    public string GroupBy { get; set; } = "day";
    public int? BranchId { get; set; }
    public List<int> ProductTypeIds { get; set; } = [];
}
