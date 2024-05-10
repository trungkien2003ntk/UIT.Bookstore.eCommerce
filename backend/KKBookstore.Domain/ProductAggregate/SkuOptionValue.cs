using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ProductAggregate;

public class SkuOptionValue : BaseEntity, ISoftDelete, ITrackable
{
    public int SkuId { get; set; }
    public int OptionId { get; set; }
    public int OptionValueId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation properties
    public Option Option { get; set; }
    public Sku Sku { get; set; }
    public OptionValue OptionValue { get; set; }
    public User LastEditedByUser { get; set; }
}
