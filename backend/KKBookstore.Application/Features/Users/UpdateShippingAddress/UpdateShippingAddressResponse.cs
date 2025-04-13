using KKBookstore.Orders;

namespace KKBookstore.Features.Users.UpdateShippingAddress;

public record UpdateShippingAddressResponse(
    int Id,
    int UserId,
    string ReceiverName,
    string PhoneNumber,
    int ProvinceId,
    string ProvinceName,
    int DistrictId,
    string DistrictName,
    string CommuneCode,
    string CommuneName,
    string DetailAddress,
    bool IsDefault,
    AddressType AddressType
);
