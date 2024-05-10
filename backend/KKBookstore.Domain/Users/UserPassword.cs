using KKBookstore.Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Domain.Users;

public class UserPassword : BaseEntity, ITrackable
{
    public int UserPasswordId { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset ChangedWhen { get; set; }

    public int LastEditedBy { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }

    // navigation property
    public User User { get; set; }
    public User LastEditedByUser { get; set; }
}
