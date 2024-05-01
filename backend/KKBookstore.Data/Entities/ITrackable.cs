namespace KKBookstore.Data.Entities;

public interface ITrackable
{
    public int LastEditedBy { get; set; }
    public User LastEditedByUser {  get; set; }
    public DateTimeOffset LastEditedWhen { get; set; }
}
