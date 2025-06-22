using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Orders.ConfirmOrderReceived;

public class ConfirmOrderReceivedCommand : IRequest<Result>
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
}
