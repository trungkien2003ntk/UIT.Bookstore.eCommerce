using KKBookstore.Domain.Common;

namespace KKBookstore.Domain.DiscountAggregate;

public static class DiscountVoucherError
{
    // list all error a discountvoucher can have using KKBookstore.Domain.Error record
    public static readonly Error InvalidCode = new("DiscountVoucher.InvalidCode", "Invalid discount voucher code");
    public static readonly Error InvalidApplyQuantityRange = new("DiscountVoucher.InvalidApplyQuantityRange", "Discount voucher quantity range to apply is invalid");
    public static readonly Error Expired = new("DiscountVoucher.Expired", "Discount voucher has expired");
    public static readonly Error NotStarted = new("DiscountVoucher.NotStarted", "Discount voucher has not started yet");
    public static readonly Error NotApplicable = new("DiscountVoucher.NotApplicable", "Discount voucher is not applicable for this order");
    public static readonly Error NotEnoughQuantity = new("DiscountVoucher.NotEnoughQuantity", "Discount voucher is not applicable for this order");
    public static readonly Error NotEnoughAmount = new("DiscountVoucher.NotEnoughAmount", "Discount voucher is not applicable for this order");
    public static readonly Error NotEnoughTotalAmount = new("DiscountVoucher.NotEnoughTotalAmount", "Discount voucher is not applicable for this order");
    public static readonly Error NotEnoughTotalQuantity = new("DiscountVoucher.NotEnoughTotalQuantity", "Discount voucher is not applicable for this order");
}
