using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Services;

public class CustomerService : ICustomerService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(IApplicationDbContext dbContext, ILogger<CustomerService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result> UpdateCustomerSpentAmountAsync(int customerId, decimal amount, CancellationToken cancellationToken = default)
    {
        try
        {
            var customer = await _dbContext.Customers
                .Include(c => c.CustomerType)
                .FirstOrDefaultAsync(c => c.Id == customerId, cancellationToken);

            if (customer == null)
            {
                _logger.LogWarning("Customer with ID {CustomerId} not found", customerId);
                return Result.Failure(Error.NotFound("Customer.NotFound", "Customer not found"));
            }

            // Get all available customer types for tier calculation
            var customerTypes = await _dbContext.CustomerTypes
                .OrderBy(ct => ct.MinSpending)
                .ToListAsync(cancellationToken);

            var updateResult = customer.UpdateSpentAmountAndTier(amount, customerTypes);
            if (updateResult.IsFailure)
            {
                return updateResult;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Updated customer {CustomerId} spent amount by {Amount}. Total spent: {TotalSpent}, Customer type: {CustomerType}",
                customerId, amount, customer.TotalSpent, customer.CustomerType?.Name ?? "None");

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update customer {CustomerId} spent amount", customerId);
            return Result.Failure(Error.Failure("Customer.UpdateFailed", "Failed to update customer spent amount"));
        }
    }
}
