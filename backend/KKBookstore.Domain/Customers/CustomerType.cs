using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Customers;

public class CustomerType : BaseFullAuditedEntity
{
    public string Name { get; set; } = null!; // e.g. "Regular", "VIP", "Premium"
    public double MinSpending { get; set; } // e.g. 0, 1000, 5000
}