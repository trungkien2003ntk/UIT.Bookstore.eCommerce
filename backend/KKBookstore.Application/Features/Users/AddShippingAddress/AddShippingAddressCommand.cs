using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.AddShippingAddress;

public record AddShippingAddressCommand(
    int UserId,
    string ReceiverName,
    string PhoneNumber,
    string Province,
    string District,
    string Commune,
    string DetailAddress,
    bool IsDefault,
    string AddressType
) : IRequest<Result<AddShippingAddressResponse>>;