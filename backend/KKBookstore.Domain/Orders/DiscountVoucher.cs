using KKBookstore.Models;
using KKBookstore.ProductTypes;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KKBookstore.Orders;


// TODO: Need more workarounds to make this class more complete
public class DiscountVoucher : BaseFullAuditedEntity
{
    public DiscountVoucher()
    {

    }    private DiscountVoucher(
        string name,
        string code,
        string description,
        DiscountValueType valueType,
        DiscountVoucherType voucherType,
        DiscountStatus status,
        decimal value,
        decimal? maximumDiscountValue,
        decimal minimumSpend,
        int? usageLimitPerUser,
        int usageLimitOverall,
        DateTimeOffset startTime,
        DateTimeOffset endTime,
        int? applyToProductTypeId = null
        ) : base()
    {
        Name = name;
        Code = code;
        Description = description;
        ValueType = valueType;
        VoucherType = voucherType;
        Status = status;
        Value = value;
        MaximumDiscountValue = maximumDiscountValue;
        MinimumSpend = minimumSpend;
        UsageLimitPerUser = usageLimitPerUser;
        UsageLimitOverall = usageLimitOverall;
        StartTime = startTime;
        EndTime = endTime;
        ApplyToProductTypeId = applyToProductTypeId;
    }    // Discount voucher basic properties
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DiscountValueType ValueType { get; set; }
    public DiscountVoucherType VoucherType { get; set; }
    public DiscountStatus Status { get; set; }
    public decimal Value { get; set; } // percentage 0.15 or fixed amount


    // Discount limit
    public decimal? MaximumDiscountValue { get; set; } // this is set only when the discount is percentage
    public decimal MinimumSpend { get; set; }


    // Usage limit
    public int? UsageLimitPerUser { get; set; }
    public int UsageLimitOverall { get; set; }


    // Time limit
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    [NotMapped]
    public bool IsRedeemable { get; set; }

    // Optional one-to-one relationship with ProductType
    public int? ApplyToProductTypeId { get; set; }
    public ProductType? ApplyToProductType { get; set; }

    // navigation property to Order and OrderLine
    public ICollection<VoucherUsage> VoucherUsages { get; set; } = [];



    public decimal GetDiscountValue(decimal spentAmount)
    {
        if (ValueType == DiscountValueType.Percentage)
        {
            return MaximumDiscountValue.HasValue ? Math.Min(MaximumDiscountValue.Value, spentAmount * Value) : spentAmount * Value;
        }

        return spentAmount > Value ? Value : spentAmount;
    }

    public bool IsApplicable(decimal spentAmount, int userId)
    {
        if (MinimumSpend > spentAmount)
        {
            return false;
        }

        if (!CheckUsage(userId))
        {
            return false;
        }

        return true;
    }

    private bool CheckUsage(int UserId)
    {
        var exceededUsageOverall = UsageLimitOverall <= VoucherUsages.Count;

        if (exceededUsageOverall)
            return false;

        if (!UsageLimitPerUser.HasValue)
        {
            return true;
        }

        var userVoucherUsageCount = VoucherUsages.Count(vu => vu.CustomerId == UserId);

        if (UsageLimitPerUser.HasValue && userVoucherUsageCount < UsageLimitPerUser)
        {
            return true;
        }

        return false;
    }    public static Result<DiscountVoucher> Create(
        string code,
        string description,
        DiscountValueType valueType,
        DiscountVoucherType voucherType,
        DiscountStatus status,
        decimal value,
        decimal? maximumDiscountValue,
        decimal minimumSpend,
        int? usageLimitPerUser,
        int usageLimitOverall,
        DateTimeOffset startWhen,
        DateTimeOffset endWhen,
        int? applyToProductTypeId = null
        )
    {
        // validation logic
        if (value < 0)
        {
            return Result.Failure<DiscountVoucher>(DiscountVoucherErrors.ValueMustBePositive);
        }

        if (value > 1)
        {
            return Result.Failure<DiscountVoucher>(DiscountVoucherErrors.InvalidValueRange);
        }

        if (valueType == DiscountValueType.Fixed)
        {
            maximumDiscountValue = null;
        }

        var name = CreateDiscountName(value, maximumDiscountValue, valueType);        return Result.Success(new DiscountVoucher(
            name,
            code,
            description,
            valueType,
            voucherType,
            status,
            value,
            maximumDiscountValue,
            minimumSpend,
            usageLimitPerUser,
            usageLimitOverall,
            startWhen,
            endWhen,
            applyToProductTypeId
        ));
    }

    private static string CreateDiscountName(decimal value, decimal? maximumDiscountValue, DiscountValueType valueType)
    {
        StringBuilder nameBuilder = new("Giảm giá ");

        if (valueType == DiscountValueType.Percentage)
        {
            int percentage = (int)(value * 100);
            nameBuilder.Append($"{percentage}%");
        }
        else
        {
            int fixedValue = (int)(value / 1000);
            nameBuilder.Append($"₫{fixedValue}k");
        }

        if (maximumDiscountValue.HasValue)
        {
            int maximumDiscountValueFixed = (int)(maximumDiscountValue.Value / 1000);
            nameBuilder.Append($", tối đa ₫{maximumDiscountValueFixed}k");
        }

        return nameBuilder.ToString();
    }
}
