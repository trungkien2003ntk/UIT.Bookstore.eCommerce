namespace KKBookstore.Application.Features.Users.UpdateShippingAddress;

public record UpdateShippingAddressResponse(
    int Id,
    int UserId,
    string ReceiverName,
    string PhoneNumber,
    string Province,
    string District,
    string Commune,
    string DetailAddress,
    bool IsDefault,
    string AddressType
);
