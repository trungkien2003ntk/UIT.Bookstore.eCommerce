using KKBookstore.Domain.Models;
using KKBookstore.Domain.ProductTypes;

namespace KKBookstore.Domain.Orders;

public class DiscountApplyToProductType : BaseFullAuditedEntity
{
    public int DiscountVoucherId { get; set; }
    public int ProductTypeId { get; set; }

    // navigation properties
    public DiscountVoucher DiscountVoucher { get; set; } = null!;
    public ProductType ProductType { get; set; } = null!;
}
