using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;

namespace KKBookstore.Features.Orders.UpdateOrderStatus;

public class UpdateOrderStatusCommand : IRequest<Result>
{
    public int OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
    public int? AdminUserId { get; set; } // Track which admin updated the status
}
