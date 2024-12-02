using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Orders;

public sealed class VoucherUsage : BaseAuditedEntity
{
    public VoucherUsage()
    {

    }

    public int VoucherId { get; set; }
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTimeOffset RedemptionTime { get; set; }

    // navigation properties
    public DiscountVoucher Voucher { get; set; } = null!;
    public Order Order { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}
