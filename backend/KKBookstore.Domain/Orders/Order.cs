using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Orders;

namespace KKBookstore.Domain.Orders;

public class Order : BaseAuditedEntity
{
    public Order()
    {
        OrderWhen = DateTimeOffset.Now;
        OrderNumber = GenerateOrderNumber();
        TaxRate = 0;
    }

    private Order(
        int customerId,
        int shippingAddressId,
        int deliveryMethodId,
        int paymentMethodId,
        DateTimeOffset orderWhen,
        decimal taxRate,
        OrderStatus status
    ) : base()
    {
        OrderNumber = GenerateOrderNumber();
        CustomerId = customerId;
        ShippingAddressId = shippingAddressId;
        DeliveryMethodId = deliveryMethodId;
        PaymentMethodId = paymentMethodId;
        OrderWhen = orderWhen;
        TaxRate = taxRate;
        Status = status;
    }

    public string OrderNumber { get; set; }
    public decimal Subtotal => OrderLines.Sum(ol => ol.Quantity * ol.UnitPrice);
    public decimal TaxRate { get; set; }
    public string? Comment { get; set; }
    public string? DeliveryInstruction { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public int ShippingAddressId { get; set; }
    public int? DeliveryMethodId { get; set; }
    public decimal ShippingFee { get; set; }
    public int? PriceDiscountVoucherId { get; set; }
    public int? ShippingDiscountVoucherId { get; set; }
    public int? PaymentMethodId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTimeOffset OrderWhen { get; set; }
    public DateTimeOffset? DueWhen { get; set; }
    public DateTimeOffset? PaidWhen { get; set; }
    public DateTimeOffset ExpectedDeliveryWhen { get; set; }
    public DateTimeOffset? PickingCompletedWhen { get; set; }
    public DateTimeOffset? ConfirmedDeliveryWhen { get; set; }
    public DateTimeOffset? ConfirmedReceivedWhen { get; set; }

    // navigation properties
    public ShippingAddress ShippingAddress { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public DeliveryMethod? DeliveryMethod { get; set; }
    public DiscountVoucher? PriceDiscountVoucher { get; set; }
    public DiscountVoucher? ShippingDiscountVoucher { get; set; }
    public Customer Customer { get; set; }
    public ICollection<OrderLine> OrderLines { get; set; } = [];
    public ICollection<Transaction> Transactions { get; set; } = [];

    public Result ApplyVoucher(DiscountVoucher voucher)
    {
        if (!voucher.IsApplicable(Subtotal, CustomerId))
        {
            return Result.Failure(OrderErrors.DiscountVoucherNotAvailable);
        }

        if (voucher.VoucherType == DiscountVoucherType.Order)
        {
            PriceDiscountVoucherId = voucher.Id;
            PriceDiscountVoucher = voucher;
        }
        else
        {
            ShippingDiscountVoucherId = voucher.Id;
            ShippingDiscountVoucher = voucher;
        }

        return Result.Success();
    }

    private string GenerateOrderNumber()
    {
        // generate order number follow pattern year-month-day/short_uid
        return $"{OrderWhen.Year}-{OrderWhen.Month}-{OrderWhen.Day}/{Guid.NewGuid().ToString()[..5]}";
    }

    public decimal CalculateTotal()
    {
        decimal shippingFee = ShippingFee;
        decimal subtotal = Subtotal;
        decimal shippingDiscount = 0m;
        decimal priceDiscount = 0m;

        if (ShippingDiscountVoucher != null)
        {
            shippingDiscount = ShippingDiscountVoucher.GetDiscountValue(shippingFee);
        }

        if (PriceDiscountVoucher != null)
        {
            priceDiscount = PriceDiscountVoucher.GetDiscountValue(subtotal);
        }

        return subtotal + shippingFee - shippingDiscount - priceDiscount;
    }

    public bool IsCompleted()
    {
        return Status == OrderStatus.Received || Status == OrderStatus.Delivered;
    }

    public static Result<Order> Create(
        int customerId,
        int shippingAddressId,
        int deliveryMethodId,
        int paymentMethodId,
        DateTimeOffset orderWhen,
        decimal taxRate,
        OrderStatus status
    )
    {

        return new Order(
            customerId,
            shippingAddressId,
            deliveryMethodId,
            paymentMethodId,
            orderWhen,
            taxRate,
            status
        );
    }
}
