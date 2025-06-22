using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Orders.ConfirmOrderReceived;

public class ConfirmOrderReceivedCommand : IRequest<Result>
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public string? FeedbackNotes { get; set; }
    public int? Rating { get; set; } // 1-5 stars
}
