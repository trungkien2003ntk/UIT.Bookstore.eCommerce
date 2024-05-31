using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.Orders.Models;

public record DeliveryMethodDto : BaseDto
{
    public string Name { get; init; }
    public string Description { get; init; }
}
