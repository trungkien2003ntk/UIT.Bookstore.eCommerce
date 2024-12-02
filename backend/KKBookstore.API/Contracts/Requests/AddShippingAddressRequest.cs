using KKBookstore.Domain.Shared.Orders;

namespace KKBookstore.API.Contracts.Requests;

public class AddShippingAddressRequest
{
    public string ReceiverName { get; init; }
    public string PhoneNumber { get; init; }
    public string Province { get; init; }
    public string District { get; init; }
    public string Commune { get; init; }
    public string DetailAddress { get; init; }
    public bool IsDefault { get; init; }
    public AddressType Type { get; init; }
}
