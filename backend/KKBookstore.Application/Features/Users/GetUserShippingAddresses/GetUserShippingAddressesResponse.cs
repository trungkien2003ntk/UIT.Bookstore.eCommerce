using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.Users.GetUserShippingAddresses;

public record GetUserShippingAddressesResponse : BaseDto
{
    public int UserId { get; set; }
    public string ReceiverName { get; set; }
    public string PhoneNumber { get; set; }
    public string Province { get; set; }
    public string District { get; set; }
    public string Commune { get; set; }
    public string DetailAddress { get; set; }
    public bool IsDefault { get; set; }
    public string AddressType { get; set; }
}
