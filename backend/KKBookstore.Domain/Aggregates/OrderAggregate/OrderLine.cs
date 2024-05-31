using KKBookstore.Domain.Aggregates.DiscountAggregate;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.OrderAggregate;


public class OrderLine : BaseAuditableEntity
{
    public OrderLine()
    {

    }
    private OrderLine(
        int skuId,
        string description,
        int quantity,
        decimal unitPrice,
        int? discountVoucherId = null
    ) : base()
    {
        SkuId = skuId;
        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
        DiscountVoucherId = discountVoucherId;
    }

    public int OrderId { get; set; }
    public int SkuId { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTimeOffset? PickingCompletedWhen { get; set; }
    public int? DiscountVoucherId { get; set; }

    // navigation property
    public Sku Sku { get; set; }
    public DiscountVoucher? DiscountVoucher { get; set; }
    public Order Order { get; set; }

    // factory method
    public static Result<OrderLine> Create(
        int skuId,
        string description,
        int quantity,
        decimal unitPrice,
        int? discountVoucherId
    )
    {
        return new OrderLine(
            skuId,
            description,
            quantity,
            unitPrice,
            discountVoucherId
        );
    }
}
