using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;

namespace KKBookstore.Domain.Orders;


public class OrderLine : BaseAuditedEntity
{
    public OrderLine()
    {

    }
    private OrderLine(
        int productVariantId,
        int quantity,
        decimal unitPrice,
        int? discountVoucherId = null
    ) : base()
    {
        ProductVariantId = productVariantId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        DiscountVoucherId = discountVoucherId;
    }

    public int OrderId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTimeOffset? PickingCompletionTime { get; set; }
    public int? DiscountVoucherId { get; set; }

    // navigation property
    public ProductVariant ProductVariant { get; set; }
    public DiscountVoucher? DiscountVoucher { get; set; }
    public Order Order { get; set; }

    // factory method
    public static Result<OrderLine> Create(
        int productVariantId,
        int quantity,
        decimal unitPrice,
        int? discountVoucherId
    )
    {
        return new OrderLine(
            productVariantId,
            quantity,
            unitPrice,
            discountVoucherId
        );
    }
}
