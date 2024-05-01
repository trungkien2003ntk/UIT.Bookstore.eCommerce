
namespace KKBookstore.Data.Entities
{
    public class OptionValue : BaseEntity, ISoftDelete, ITrackable
    {
        public int OptionValueID { get; set; }
        public int OptionID { get; set; }
        public string Name { get; set; }
        public string LargeImageUrl { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedWhen { get; set; }
        public int LastEditedBy { get; set; }
        public User LastEditedByUser { get; set; }
        public DateTimeOffset LastEditedWhen { get; set; }
    }
}
