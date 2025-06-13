namespace KKBookstore.Emailing.TemplateModels;

public class OrderConfirmationEmailModel : IEmailModel
{
    public string TemplateName => EmailConsts.OrderConfirmationEmailTemplateName;

    public string Subject => EmailConsts.OrderConfirmationEmailSubject;

    public string? ReceiverFullName { get; set; }

    public int OrderId { get; }
    public string OrderNumber { get; }
    public decimal TotalAmount { get; }
    public DateTimeOffset OrderDate { get; }
    public DateTimeOffset ExpectedDeliveryDate { get; }
    public string? Note { get; }
    public List<OrderLineItem> OrderItems { get; }
    public decimal ShippingFee { get; }
    public decimal? DiscountAmount { get; }
    public string ShippingAddress { get; }
    public string PaymentMethod { get; }
    public string DeliveryMethod { get; }

    public object TemplateDataModel => new
    {
        recipient_name = ReceiverFullName ?? "Khách hàng",
        order_id = OrderId,
        order_number = OrderNumber,
        total_amount = TotalAmount,
        order_date = OrderDate.ToString("dd/MM/yyyy HH:mm"),
        expected_delivery_date = ExpectedDeliveryDate.ToString("dd/MM/yyyy"),
        note = Note ?? "",
        order_items = OrderItems.Select(item => new
        {
            product_name = item.ProductName,
            variant_name = item.VariantName,
            quantity = item.Quantity,
            unit_price = item.UnitPrice,
            total_price = item.Quantity * item.UnitPrice,
            thumbnail_url = item.ThumbnailUrl
        }),
        shipping_fee = ShippingFee,
        discount_amount = DiscountAmount ?? 0,
        shipping_address = ShippingAddress,
        payment_method = PaymentMethod,
        delivery_method = DeliveryMethod,
        subtotal = OrderItems.Sum(item => item.Quantity * item.UnitPrice)
    };

    public OrderConfirmationEmailModel(
        int orderId,
        string orderNumber,
        decimal totalAmount,
        DateTimeOffset orderDate,
        DateTimeOffset expectedDeliveryDate,
        List<OrderLineItem> orderItems,
        decimal shippingFee,
        string shippingAddress,
        string paymentMethod,
        string deliveryMethod,
        string? receiverFullName = null,
        string? note = null,
        decimal? discountAmount = null)
    {
        OrderId = orderId;
        OrderNumber = orderNumber;
        TotalAmount = totalAmount;
        OrderDate = orderDate;
        ExpectedDeliveryDate = expectedDeliveryDate;
        OrderItems = orderItems;
        ShippingFee = shippingFee;
        ShippingAddress = shippingAddress;
        PaymentMethod = paymentMethod;
        DeliveryMethod = deliveryMethod;
        ReceiverFullName = receiverFullName;
        Note = note;
        DiscountAmount = discountAmount;
    }
}

public class OrderLineItem
{
    public string ProductName { get; set; } = string.Empty;
    public string VariantName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? ThumbnailUrl { get; set; }
}
