using KKBookstore.Models;
using KKBookstore.ProductTypes;

namespace KKBookstore.Orders;

public class DiscountApplyToProductType : BaseFullAuditedEntity
{
    public int DiscountVoucherId { get; set; }
    public int ProductTypeId { get; set; }

    // navigation properties
    public DiscountVoucher DiscountVoucher { get; set; } = null!;
    public ProductType ProductType { get; set; } = null!;
}
