using KKBookstore.Orders;

namespace KKBookstore.Contracts.Requests;

public class UpdateOrderStatusRequest
{
    public OrderStatus Status { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
}
