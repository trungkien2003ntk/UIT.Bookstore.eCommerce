using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Users;

namespace KKBookstore.Customers;

public class Customer : User
{
    public int? CustomerTypeId { get; set; }

    public CustomerType? CustomerType { get; set; }
    public ICollection<ShippingAddress>? ShippingAddresses { get; set; }
}