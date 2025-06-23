using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Orders;

namespace KKBookstore.Features.DiscountVouchers.Models;

public record DiscountVoucherDto : BaseFullAuditedDto
{
    public string Name { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DiscountValueType ValueType { get; init; }
    public DiscountVoucherType VoucherType { get; init; }
    public DiscountStatus Status { get; init; }
    public decimal Value { get; init; }
    public decimal? MaximumDiscountValue { get; init; }
    public decimal MinimumSpend { get; init; }
    public int? UsageLimitPerUser { get; init; }
    public int UsageLimitOverall { get; init; }
    public DateTimeOffset StartTime { get; init; }
    public DateTimeOffset EndTime { get; init; }

    // Product Type fields - new approach
    public string? ApplyToProductTypeIds { get; init; }
    public List<int> ApplyToProductTypeIdsList { get; init; } = [];
    public List<ApplyToProductTypeDto> ApplyToProductTypes { get; init; } = [];

    // Legacy fields for backward compatibility
    public List<int> CustomerTypeIds { get; init; } = [];
    public List<string> CustomerTypeNames { get; init; } = [];
    public int UsageCount { get; init; }
    public decimal UsedPercentage { get; init; }
    public List<CustomerTypeDto> CustomerTypes { get; init; } = [];

    // Cart integration - indicates if voucher can be applied to selected cart items
    public bool? CanApply { get; init; }
}

public class CustomerTypeDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public class ApplyToProductTypeDto
{
    public int Id { get; init; }
    public string DisplayName { get; init; } = string.Empty;
}