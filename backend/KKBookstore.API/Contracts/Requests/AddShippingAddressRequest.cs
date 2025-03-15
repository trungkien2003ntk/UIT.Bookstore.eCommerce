using KKBookstore.Domain.Shared.Orders;

namespace KKBookstore.API.Contracts.Requests;

public class AddShippingAddressRequest
{
    public string ReceiverName { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public int ProvinceId { get; init; }
    public string ProvinceName { get; init; } = null!;
    public int DistrictId { get; init; }
    public string DistrictName { get; init; } = null!;
    public string CommuneCode { get; init; } = null!;
    public string CommuneName { get; init; } = null!;
    public string DetailAddress { get; init; } = null!;
    public bool IsDefault { get; init; }
    public AddressType Type { get; init; }
}
