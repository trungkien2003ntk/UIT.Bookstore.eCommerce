using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.Orders.Models;

public record ShippingAddressDto : BaseDto
{
    public string ReceiverName { get; set; }
    public string PhoneNumber { get; set; }
    public string DetailedFullAddress { get; set; }
}
