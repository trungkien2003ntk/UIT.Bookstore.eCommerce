using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.Checkout.Confirm;

public record ConfirmCheckoutResponse
{
    //Order summary
    //shipping_addresses
    //payment_methods
    //checkout_items
    public OrderPriceSummary PriceSummary { get; init; }
    public List<ShippingAddressDto> ShippingAddresses { get; init; } = [];
    public List<PaymentMethodDto> PaymentMethods { get; init; } = [];
    public List<DeliveryMethodDto> DeliveryMethods { get; init; } = [];
    public List<CheckoutItemDto> Items { get; init; } = [];

    public sealed record OrderPriceSummary
    {
        public decimal Subtotal { get; init; }
        public decimal ShippingFee { get; init; }
        public decimal ShippingDiscount { get; init; }
        public decimal OrderVoucherDiscount { get; init; }
        public decimal Total { get; init; }
    }

    public sealed record ShippingAddressDto : BaseDto
    {
        public int UserId { get; init; }
        public string ReceiverName { get; init; }
        public string PhoneNumber { get; init; }
        public string Province { get; init; }
        public string District { get; init; }
        public string Commune { get; init; }
        public string DetailAddress { get; init; }
        public bool IsDefault { get; init; }
        public string AddressType { get; init; }
    }

    public sealed record PaymentMethodDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed record DeliveryMethodDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ExpectedDeliveryWhen { get; set; }
    }

    public sealed record CheckoutItemDto : BaseDto
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; }
        public int ProductVariantId { get; init; }
        public string ProductVariantName { get; init; }
        public string? ImageUrl { get; init; }
        public decimal UnitPrice { get; init; }
        public int Quantity { get; init; }
        public decimal TotalPrice { get; init; }
    }
}
