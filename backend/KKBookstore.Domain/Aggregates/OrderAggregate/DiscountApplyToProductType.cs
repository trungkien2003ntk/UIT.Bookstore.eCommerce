using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.OrderAggregate;

public class DiscountApplyToProductType : BaseAuditableEntity, ISoftDelete
{
    public int DiscountVoucherId { get; set; }
    public int ProductTypeId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation properties
    public DiscountVoucher DiscountVoucher { get; set; }
    public ProductType ProductType { get; set; }

}
