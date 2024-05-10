namespace KKBookstore.Domain.Common;

public abstract class BaseEntity : IEquatable<BaseEntity>
{
    protected BaseEntity(int id)
    {
        Id = id;
    }

    public int Id { get; }

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

    public static bool operator ==(BaseEntity left, BaseEntity right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    public static bool operator !=(BaseEntity left, BaseEntity right)
    {
        return !(left == right);
    }



    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
