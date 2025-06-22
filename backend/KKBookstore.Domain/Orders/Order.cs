using KKBookstore.Customers;
using KKBookstore.Models;

namespace KKBookstore.Orders;

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
    public int? ShippingAddressId { get; set; }
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
    public DateTimeOffset? ConfirmedReceivedWhen { get; set; }    // navigation properties
    public ShippingAddress ShippingAddress { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public DeliveryMethod? DeliveryMethod { get; set; }
    public DiscountVoucher? PriceDiscountVoucher { get; set; }
    public DiscountVoucher? ShippingDiscountVoucher { get; set; }
    public Customer Customer { get; set; }    public ICollection<OrderLine> OrderLines { get; set; } = [];
    public ICollection<Transaction> Transactions { get; set; } = [];
    public ICollection<OrderFulfillment> OrderFulfillments { get; set; } = [];
    public ICollection<OrderHistory> OrderHistories { get; set; } = [];

    public Result ApplyVoucher(DiscountVoucher voucher)
    {
        var distinctProductTypeIds = OrderLines
            .Select(ol => ol.ProductVariant?.Product?.ProductTypeId ?? 0)
            .Distinct()
            .ToList();

        if (!voucher.IsApplicable(Subtotal, CustomerId, distinctProductTypeIds))
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
    }    private string GenerateOrderNumber()
    {
        // Generate order number with format: SO-{yyMMdd}<HCM>{unique}
        // SO = Sales Order prefix for e-commerce
        // yyMMdd = Order date (2-digit year, month, day) for easy tracking
        // HCM = Ho Chi Minh branch code (primary branch)
        // unique = Timestamp-based unique identifier to avoid collisions
        
        var datePrefix = OrderWhen.ToString("yyMMdd");
        var branchCode = "HCM"; // Primary branch code for Ho Chi Minh
        
        // Use timestamp + random component for uniqueness within the same millisecond
        var timestamp = DateTimeOffset.Now.ToString("HHmmssff"); // Hours, minutes, seconds, centiseconds
        var randomComponent = new Random().Next(10, 99); // 2-digit random number
        var uniqueComponent = $"{timestamp}{randomComponent}";
        
        return $"SO-{datePrefix}{branchCode}{uniqueComponent}";
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

    public bool RequiresAdminBranchSelection()
    {
        return Status == OrderStatus.WaitForConfirmPackageBranch;
    }

    public bool CanStartPackaging()
    {
        return Status == OrderStatus.WaitForConfirmPackageBranch || Status == OrderStatus.Packaging;
    }

    public bool CanConfirmReceived()
    {
        return Status == OrderStatus.Delivered;
    }

    public Result SelectBranchForPackaging(int branchId)
    {
        if (!RequiresAdminBranchSelection())
        {
            return Result.Failure(Error.Validation("Order.InvalidStatus", 
                "Order must be waiting for branch confirmation to select packaging branch"));
        }

        var fulfillment = OrderFulfillments.FirstOrDefault(of => of.BranchId == branchId);
        if (fulfillment == null)
        {
            return Result.Failure(Error.Validation("OrderFulfillment.NotFound", 
                "No inventory allocation found for the selected branch"));
        }

        fulfillment.SelectForPackaging();
        Status = OrderStatus.Packaging;

        return Result.Success();
    }

    public Result StartShipping(string ghnOrderCode)
    {
        if (Status != OrderStatus.Packaging)
        {
            return Result.Failure(Error.Validation("Order.InvalidStatus", 
                "Order must be in packaging status to start shipping"));
        }

        Status = OrderStatus.Shipped;
        Comment = string.IsNullOrEmpty(Comment) 
            ? $"GHN Order Code: {ghnOrderCode}" 
            : $"{Comment}\nGHN Order Code: {ghnOrderCode}";

        return Result.Success();
    }

    public Result ConfirmReceived()
    {
        if (!CanConfirmReceived())
        {
            return Result.Failure(Error.Validation("Order.InvalidStatus", 
                "Order must be delivered before it can be confirmed as received"));
        }

        Status = OrderStatus.Received;
        ConfirmedReceivedWhen = DateTimeOffset.Now;

        return Result.Success();
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
