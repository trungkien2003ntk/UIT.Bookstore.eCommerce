namespace KKBookstore.Domain.Models;

public abstract class BaseAggregateRoot(int id) : BaseAuditableEntity(id)
{
}
