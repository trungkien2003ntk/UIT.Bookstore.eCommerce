using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.ProductAggregate
{
    public class OptionValue : BaseEntity, ISoftDelete, ITrackable
    {
        public int OptionValueId { get; set; }
        public int OptionId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedWhen { get; set; }
        public int LastEditedBy { get; set; }
        public User LastEditedByUser { get; set; }
        public DateTimeOffset LastEditedWhen { get; set; }

        //navigation properties
        public Option Option { get; set; }
        public ICollection<SkuOptionValue> SkuOptionVales { get; set; } = new List<SkuOptionValue>();
    }
}
