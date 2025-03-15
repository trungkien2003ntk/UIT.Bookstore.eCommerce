using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Domain.Shared.Orders;

namespace KKBookstore.Application.Features.Users.GetUserShippingAddresses;

public record GetUserShippingAddressesResponse : BaseDto
{
    public int CustomerId { get; set; }
    public string ReceiverName { get; set; }
    public string PhoneNumber { get; set; }
    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; }
    public int DistrictId { get; set; }
    public string DistrictName { get; set; }
    public string CommuneCode { get; set; }
    public string CommuneName { get; set; }
    public string DetailAddress { get; set; }
    public bool IsDefault { get; set; }
    public AddressType Type { get; set; }
}
