namespace KKBookstore.Domain.Models;

public abstract class BaseEntity : IEquatable<BaseEntity>
{
    protected BaseEntity(int id)
    {
        Id = id;
    }

    protected BaseEntity()
    {

    }

    public int Id { get; set; }

    // implement IEquatable<BaseEntity>
    public bool Equals(BaseEntity? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        if (obj is not BaseEntity entity)
            return false;

        return Id == entity.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
