using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Data.Entities;

public class UserPassword: BaseEntity, ITrackable
{
    public int UserPasswordID { get; set; }
    public int UserID { get; set; }
    public DateTimeOffset ChangedWhen { get; set; }
    
    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation property
    public User User { get; set; }
    public User LastEditedByUser { get; set; }
}
