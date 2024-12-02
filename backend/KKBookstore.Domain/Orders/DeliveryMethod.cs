using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Orders;

public class DeliveryMethod : BaseFullAuditedEntity
{
    public DeliveryMethod()
    {
        Name = "";
        Description = "";
    }
    private DeliveryMethod(
        string name,
        string description
    ) : base()
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string? Description { get; set; }

    // factory method
    public static DeliveryMethod Create(
        string name,
        string description
    )
    {
        return new DeliveryMethod(name, description);
    }
}
