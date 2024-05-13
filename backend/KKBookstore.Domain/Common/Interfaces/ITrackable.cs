using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Common.Interfaces;

public interface ITrackable
{
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
}
