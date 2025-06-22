namespace KKBookstore.Emailing.TemplateModels;

public class OrderConfirmationEmailModel : IEmailModel
{
    public string TemplateName => EmailConsts.OrderConfirmationEmailTemplateName;

    public string Subject => EmailConsts.OrderConfirmationEmailSubject;

    public string? ReceiverFullName { get; set; }

    public int OrderId { get; }
    public string OrderNumber { get; }
    public decimal TotalAmount { get; }
    public string TotalAmountFormatted => TotalAmount.ToString("N0") + " ₫";
    public DateTimeOffset OrderDate { get; }
    public DateTimeOffset ExpectedDeliveryDate { get; }
    public string? Note { get; }
    public List<OrderLineItem> OrderItems { get; }
    public decimal ShippingFee { get; }
    public string ShippingFeeFormatted => ShippingFee.ToString("N0") + " ₫";
    public decimal? DiscountAmount { get; }
    public string DiscountAmountFormatted => DiscountAmount.HasValue ? DiscountAmount.Value.ToString("N0") + " ₫" : "0 ₫";
    public string ShippingAddress { get; }
    public string PaymentMethod { get; }
    public string DeliveryMethod { get; }
    public object TemplateDataModel => new
    {
        RecipientName = ReceiverFullName ?? "Khách hàng",
        OrderId = OrderId,
        OrderNumber = OrderNumber,
        TotalAmount = TotalAmount,
        OrderDate = OrderDate.ToString("dd/MM/yyyy HH:mm"),
        ExpectedDeliveryDate = ExpectedDeliveryDate.ToString("dd/MM/yyyy"),
        Note = Note ?? "",
        OrderItems = OrderItems.Select(item => new
        {
            ProductName = item.ProductName,
            VariantName = item.VariantName,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            UnitPriceFormatted = item.UnitPriceFormatted,
            RecommendedRetailPrice = item.RecommendedRetailPrice,
            RecomendedRetailPriceFormatted = item.RecommendedRetailPriceFormatted,
            TotalPrice = item.Quantity * item.RecommendedRetailPrice,
            TotalPriceFormatted = (item.Quantity * item.RecommendedRetailPrice).ToString("N0") + " ₫",
            ThumbnailUrl = item.ThumbnailUrl
        }),
        ShippingFee = ShippingFee,
        DiscountAmount = DiscountAmount ?? 0,
        DiscountAmountFormatted = DiscountAmountFormatted,
        ShippingAddress = ShippingAddress,
        PaymentMethod = PaymentMethod,
        DeliveryMethod = DeliveryMethod,
        SubtotalFormatted = OrderItems.Sum(item => item.Quantity * item.RecommendedRetailPrice).ToString("N0") + " ₫",
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
    public string UnitPriceFormatted => UnitPrice.ToString("N0") + " ₫";
    public decimal RecommendedRetailPrice { get; set; }
    public string RecommendedRetailPriceFormatted => RecommendedRetailPrice.ToString("N0") + " ₫";
    public string? ThumbnailUrl { get; set; }
}
