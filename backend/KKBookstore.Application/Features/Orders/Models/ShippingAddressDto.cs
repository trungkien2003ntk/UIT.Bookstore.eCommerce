using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Orders.Models;

public record ShippingAddressDto : BaseDto
{
    public string ReceiverName { get; set; }
    public string PhoneNumber { get; set; }
    public string DetailedFullAddress { get; set; }
}
