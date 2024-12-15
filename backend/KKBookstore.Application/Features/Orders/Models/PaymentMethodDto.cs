using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Orders.Models;

public record PaymentMethodDto : BaseDto
{
    public string Name { get; init; }
    public string Description { get; init; }
}
