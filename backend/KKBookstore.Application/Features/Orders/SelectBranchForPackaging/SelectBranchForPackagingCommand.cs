using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Orders.SelectBranchForPackaging;

public class SelectBranchForPackagingCommand : IRequest<Result>
{
    public int OrderId { get; set; }
    public int BranchId { get; set; }
    public string? Notes { get; set; }
    public int? AdminUserId { get; set; } // Track which admin made the selection
}
