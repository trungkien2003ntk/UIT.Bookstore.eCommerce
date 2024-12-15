using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Domain.Shared.Orders;

namespace KKBookstore.Application.Features.DiscountVouchers.GetAllDiscountVouchersForCart;

public record GetAllDiscountVouchersForCartResponse
{
    public IEnumerable<DiscountVoucherDto> ShippingVouchers { get; init; } = [];
    public IEnumerable<DiscountVoucherDto> OrderVouchers { get; init; } = [];

    public record DiscountVoucherDto : BaseDto
    {
        public string Name { get; init; }
        public string Code { get; init; }
        public DiscountValueType ValueType { get; init; }
        public decimal Value { get; init; }
        public decimal? MaximumDiscountValue { get; init; }
        public decimal MinimumSpend { get; init; }
        public DateTimeOffset StartDate { get; init; }
        public DateTimeOffset EndDate { get; init; }
        public int UsageLimitOverall { get; init; }
        public int? UsageLimitPerUser { get; init; }
        public int UsageCount { get; init; }
        public decimal UsedPercentage { get; init; }
        public bool IsRedeemable { get; init; }
        public string VoucherType { get; init; }
    }
}
