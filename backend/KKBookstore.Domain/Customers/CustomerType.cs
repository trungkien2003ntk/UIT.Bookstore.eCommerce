using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Customers;

namespace KKBookstore.Domain.Customers;

public class CustomerType : BaseFullAuditedEntity
{
    public string Name { get; set; } = null!; // e.g Thường, Bạc, Vàng, Kim cương
    public CustomerTier Tier { get; set; } // e.g. Regular, Silver, Gold, Diamond
    public double MinSpending { get; set; } // e.g. đ0; đ1,000,000; đ5,000,000; đ10,000,000
}