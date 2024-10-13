using KKBookstore.Domain.Aggregates.UserAggregate;

namespace KKBookstore.Domain.Interfaces;

public interface ITrackable
{
    public DateTimeOffset CreatedWhen { get; set; }
    public int CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; }

    public int LastEditedByUserId { get; set; }
    public User LastEditedByUser { get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
}
