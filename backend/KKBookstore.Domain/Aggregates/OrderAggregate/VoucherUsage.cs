using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.OrderAggregate;

public sealed class VoucherUsage : BaseAuditableEntity
{
    public VoucherUsage()
    {

    }

    public int VoucherId { get; set; }
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset UsedWhen { get; set; }

    // navigation properties
    public DiscountVoucher Voucher { get; set; }
    public Order Order { get; set; }
    public User User { get; set; }
}
