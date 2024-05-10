using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ProductAggregate;

public class Option : BaseEntity, ISoftDelete, ITrackable
{
    public int OptionId { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedWhen { get; set; }
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation properties
    public Product Product { get; set; }
    public User LastEditedByUser { get; set; }
    public ICollection<SkuOptionValue> skuOptionValues { get; set; } = new List<SkuOptionValue>();
    public ICollection<OptionValue> OptionValues { get; set; } = new List<OptionValue>();
}
