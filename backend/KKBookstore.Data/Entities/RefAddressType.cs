namespace KKBookstore.Data.Entities;

public class RefAddressType : ISoftDelete, ITrackable
{
    public string RefAddressTypeCode { get ; set ; }
    public string Description { get ; set ; }
    public int LastEditedBy { get ; set ; }
    public DateTimeOffset LastEditedWhen { get ; set ; }
    public bool IsDeleted { get ; set ; }
    public DateTimeOffset? DeletedWhen { get ; set ; }

    // navigation property
    public User LastEditedByUser { get; set; }
}
