using KKBookstore.Domain.Aggregates.UserAggregate;

namespace KKBookstore.Domain.Interfaces;

public interface ITrackable
{
    public int LastEditedBy { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
}
