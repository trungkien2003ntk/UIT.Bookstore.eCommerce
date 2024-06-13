using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.AddShippingAddress;

public record AddShippingAddressCommand : IRequest<Result<AddShippingAddressResponse>>
{
    public int UserId { get; init; }
    public string ReceiverName { get; init; }
    public string PhoneNumber { get; init; }
    public string Province { get; init; }
    public string District { get; init; }
    public string Commune { get; init; }
    public string DetailAddress { get; init; }
    public bool IsDefault { get; init; }
    public string AddressType {  get; init; }
}