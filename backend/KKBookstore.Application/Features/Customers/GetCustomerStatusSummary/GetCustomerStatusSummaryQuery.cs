using KKBookstore.Common.Interfaces;
using KKBookstore.Customers;
using KKBookstore.Models;
using KKBookstore.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Customers.GetCustomerStatusSummary;

public record GetCustomerStatusSummaryQuery : IRequest<Result<CustomerStatusSummaryReport>>;

public class GetCustomerStatusSummaryQueryHandler : IRequestHandler<GetCustomerStatusSummaryQuery, Result<CustomerStatusSummaryReport>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetCustomerStatusSummaryQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<CustomerStatusSummaryReport>> Handle(
        GetCustomerStatusSummaryQuery request,
        CancellationToken cancellationToken)
    {
        // Query all customers
        var customers = await _dbContext.Users
            .OfType<Customer>()
            .AsNoTracking()
            .Include(c => c.CustomerType)
            .Where(c => c.CustomerType != null && c.CustomerType.IsDeleted == false) // Ensure customer type is not null
            .ToListAsync(cancellationToken);

        // Group by status and count
        var statusCounts = customers
            .GroupBy(c => c.Status)
            .ToDictionary(g => g.Key, g => g.Count());

        var summary = new CustomerStatusSummaryReport
        {
            // Total count
            TotalCount = customers.Count,

            // Individual status counts
            ActiveCount = statusCounts.GetValueOrDefault(UserStatus.Active, 0),
            VerifiedCount = statusCounts.GetValueOrDefault(UserStatus.Verified, 0),
            UnverifiedCount = statusCounts.GetValueOrDefault(UserStatus.Unverified, 0),
            InactiveCount = statusCounts.GetValueOrDefault(UserStatus.Inactive, 0),
            SuspendedCount = statusCounts.GetValueOrDefault(UserStatus.Suspended, 0),
            BlockedCount = statusCounts.GetValueOrDefault(UserStatus.Blocked, 0)
        };

        return Result.Success(summary);
    }
}
