using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.OrderAggregate;
using KKBookstore.Domain.ProductAggregate;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.DiscountAggregate;

public class DiscountVoucher : BaseAuditableEntity, ISoftDelete
{
    public DiscountVoucher(
        int id,
        string name,
        string description,
        bool isPercentage,
        bool isShippingVoucher,
        int? minApplyQuantity,
        int? maxApplyQuantity,
        int? minValueRange,
        int maxValueRange,
        DateTimeOffset startWhen,
        DateTimeOffset endWhen,
        int value,
        int? productId
        ) : base(id)
    {
        Name = name;
        Description = description;
        IsPercentage = isPercentage;
        IsShippingVoucher = isShippingVoucher;
        QuantityRange = DiscountQuantityRange.Create(minApplyQuantity, maxApplyQuantity);
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPercentage { get; set; }
    public bool IsShippingVoucher { get; set; }
    public DiscountQuantityRange QuantityRange { get; set; }
    public DiscountValueRange ValueRange { get; set; }
    public int Value { get; set; }
    public DateTimeOffset StartWhen { get; set; }
    public DateTimeOffset EndWhen { get; set; }
    public int? ProductId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
    
    // navigation property to Order and OrderLine
    public Product? Product { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

}
