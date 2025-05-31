namespace KKBookstore.Features.Customers.GetCustomerStatusSummary;

public record CustomerStatusSummaryReport
{
    public int TotalCount { get; init; }
    public int ActiveCount { get; init; }
    public int VerifiedCount { get; init; }
    public int UnverifiedCount { get; init; }
    public int InactiveCount { get; init; }
    public int SuspendedCount { get; init; }
    public int BlockedCount { get; init; }
}
