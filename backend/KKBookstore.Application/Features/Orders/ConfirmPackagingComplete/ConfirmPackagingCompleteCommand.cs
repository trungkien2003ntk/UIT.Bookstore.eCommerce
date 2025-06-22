using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Orders.ConfirmPackagingComplete;

public class ConfirmPackagingCompleteCommand : IRequest<Result<ConfirmPackagingCompleteResponse>>
{
    public int OrderId { get; set; }
    public string? Notes { get; set; }
    public int? AdminUserId { get; set; } // Track which admin confirmed packaging
}

public class ConfirmPackagingCompleteResponse
{
    public string GhnOrderCode { get; set; } = string.Empty;
    public string GhnTrackingUrl { get; set; } = string.Empty;
    public DateTime? ExpectedDeliveryTime { get; set; }
    public decimal ShippingCost { get; set; }
}
