using KKBookstore.Models;

namespace KKBookstore.Common.Interfaces;

public interface ICustomerService
{
    Task<Result> UpdateCustomerSpentAmountAsync(int customerId, decimal amount, CancellationToken cancellationToken = default);
}
