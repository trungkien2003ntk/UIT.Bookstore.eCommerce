using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Customers;

public class Customer : User
{
    public int? CustomerTypeId { get; set; }

    public CustomerType? CustomerType { get; set; }
    public ICollection<ShippingAddress>? ShippingAddresses { get; set; }
}