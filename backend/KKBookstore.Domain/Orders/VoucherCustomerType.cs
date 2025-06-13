using KKBookstore.Customers;
using KKBookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Orders;

[PrimaryKey(nameof(VoucherId), nameof(CustomerTypeId))]
public class VoucherCustomerType : BaseAuditedEntity
{
    public int VoucherId { get; set; }
    public DiscountVoucher Voucher { get; set; } = null!;

    public int CustomerTypeId { get; set; }
    public CustomerType CustomerType { get; set; } = null!;
}
