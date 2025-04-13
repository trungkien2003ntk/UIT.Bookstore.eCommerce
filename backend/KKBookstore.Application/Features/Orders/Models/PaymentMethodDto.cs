using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Features.Orders.Models;

public record PaymentMethodDto : BaseDto
{
    public string Name { get; init; }
    public string Description { get; init; }
}
