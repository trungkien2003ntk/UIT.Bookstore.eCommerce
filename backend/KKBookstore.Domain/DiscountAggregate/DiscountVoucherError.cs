using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.DiscountAggregate;

public static class DiscountVoucherError
{
    // list all error a discountvoucher can have using KKBookstore.Domain.Error record
    public static readonly Error InvalidCode = Error.Validation("DiscountVoucher.InvalidCode", "Invalid discount voucher code");
    public static readonly Error InvalidValueRange = Error.Validation("DiscountVoucher.InvalidValueRange", "Discount voucher value range is invalid");
    public static readonly Error InvalidApplyQuantityRange = Error.Validation("DiscountVoucher.InvalidApplyQuantityRange", "Discount voucher quantity range to apply is invalid");
    public static readonly Error Expired = Error.BusinessRuleViolation("DiscountVoucher.Expired", "Discount voucher has expired");
    public static readonly Error NotStarted = Error.BusinessRuleViolation("DiscountVoucher.NotStarted", "Discount voucher has not started yet");
    public static readonly Error NotApplicable = Error.BusinessRuleViolation("DiscountVoucher.NotApplicable", "Discount voucher is not applicable for this order");
    public static readonly Error NotEnoughQuantity = Error.BusinessRuleViolation("DiscountVoucher.NotEnoughQuantity", "Discount voucher is not applicable for this order");
    public static readonly Error NotEnoughAmount = Error.BusinessRuleViolation("DiscountVoucher.NotEnoughAmount", "Discount voucher is not applicable for this order");
    public static readonly Error NotEnoughTotalAmount = Error.BusinessRuleViolation("DiscountVoucher.NotEnoughTotalAmount", "Discount voucher is not applicable for this order");
    public static readonly Error NotEnoughTotalQuantity = Error.BusinessRuleViolation("DiscountVoucher.NotEnoughTotalQuantity", "Discount voucher is not applicable for this order");
}
