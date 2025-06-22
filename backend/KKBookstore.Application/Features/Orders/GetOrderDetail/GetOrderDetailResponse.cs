using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Features.Orders.Models;

namespace KKBookstore.Features.Orders.GetOrderDetail;

public record GetOrderDetailResponse : BaseDto
{
    public required string OrderNumber { get; init; }
    public DateTimeOffset OrderWhen { get; init; }
    public DateTimeOffset? DueWhen { get; init; }
    public DateTimeOffset? PaidWhen { get; init; }
    public DateTimeOffset ExpectedDeliveryWhen { get; init; }
    public DateTimeOffset? PickingCompletedWhen { get; init; }
    public DateTimeOffset? ConfirmedDeliveryWhen { get; init; }
    public DateTimeOffset? ConfirmedReceivedWhen { get; init; }

    public required string Status { get; init; }
    public int CustomerId { get; init; }
    public string? Comment { get; init; }
    public string? DeliveryInstruction { get; init; }

    // Price Summary - similar to ConfirmCheckoutResponse
    public required OrderPriceSummary PriceSummary { get; init; }

    // Address and Methods
    public required DeliveryMethodDto DeliveryMethod { get; init; }
    public required PaymentMethodDto PaymentMethod { get; init; }
    public required ShippingAddressDto ShippingAddress { get; init; }

    // Vouchers (full details, not just IDs)
    public DiscountVoucherDto? PriceDiscountVoucher { get; init; }
    public DiscountVoucherDto? ShippingDiscountVoucher { get; init; }

    // Order Lines
    public IEnumerable<OrderLineDto> OrderLines { get; init; } = [];

    // Fulfillment Information
    public IEnumerable<OrderFulfillmentDto> Fulfillments { get; init; } = [];

    // Customer Information (denormalized for convenience)
    public required CustomerSummaryDto Customer { get; init; }

    public sealed record OrderPriceSummary
    {
        public decimal Subtotal { get; init; }
        public decimal ProductDiscount { get; init; }
        public decimal TaxRate { get; init; }
        public decimal TaxAmount { get; init; }
        public decimal ShippingFee { get; init; }
        public decimal OrderVoucherDiscount { get; init; }
        public decimal ShippingDiscount { get; init; }
        public decimal Total { get; init; }
        public decimal PaidAmount { get; init; }
        public decimal RemainingAmount { get; init; }
    }

    public sealed record DiscountVoucherDto : BaseDto
    {
        public required string Name { get; init; }
        public required string Code { get; init; }
        public required string VoucherType { get; init; }
        public decimal DiscountValue { get; init; }
        public decimal MaxDiscountValue { get; init; }
        public bool IsPercentage { get; init; }
    }

    public sealed record OrderFulfillmentDto : BaseDto
    {
        public int BranchId { get; init; }
        public required string BranchName { get; init; }
        public required string BranchAddress { get; init; }
        public required string Status { get; init; }
        public int AllocatedQuantity { get; init; }
        public int FulfilledQuantity { get; init; }
        public DateTimeOffset? PackagingStartedWhen { get; init; }
        public DateTimeOffset? PackagingCompletedWhen { get; init; }
        public string? TrackingNumber { get; init; }
        public string? ShippingCarrier { get; init; }
    }

    public sealed record CustomerSummaryDto : BaseDto
    {
        public required string FullName { get; init; }
        public required string Email { get; init; }
        public required string PhoneNumber { get; init; }
        public required string CustomerType { get; init; }
    }
}
