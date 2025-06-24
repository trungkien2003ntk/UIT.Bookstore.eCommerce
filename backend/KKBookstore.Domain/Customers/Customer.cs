using KKBookstore.Models;
using KKBookstore.Users;

namespace KKBookstore.Customers;

public class Customer : User
{
    public int? CustomerTypeId { get; set; }
    public decimal TotalSpent { get; set; } = 0m;

    public CustomerType? CustomerType { get; set; }
    public ICollection<ShippingAddress>? ShippingAddresses { get; set; }

    public Result UpdateSpentAmountAndTier(decimal amount, ICollection<CustomerType> availableCustomerTypes)
    {
        if (amount <= 0)
        {
            return Result.Failure(Error.Validation("Customer.InvalidAmount", "Amount must be greater than zero"));
        }

        TotalSpent += amount;

        // Update customer type based on total spent
        var newCustomerType = availableCustomerTypes
            .Where(ct => TotalSpent >= (decimal)ct.MinSpending)
            .MaxBy(ct => ct.MinSpending);

        if (newCustomerType != null && newCustomerType.Id != CustomerTypeId)
        {
            CustomerTypeId = newCustomerType.Id;
            CustomerType = newCustomerType;
        }

        return Result.Success();
    }
}