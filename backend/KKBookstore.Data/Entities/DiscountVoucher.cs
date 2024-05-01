
namespace KKBookstore.Data.Entities;

public class DiscountVoucher : BaseEntity, ISoftDelete, ITrackable
{
    public int DiscountVoucherID {  get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPercentage { get; set; }
    public bool IsShippingVoucher { get; set; }
    public int MinApplyQuantity { get; set; }
    public int MaxApplyQuantity { get; set; }
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    public int Value { get; set; }
    public DateTimeOffset StartWhen { get; set; }
    public DateTimeOffset EndWhen { get; set; }
    public int? ProductID { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
}
