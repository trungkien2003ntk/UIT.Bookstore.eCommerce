using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Suppliers;

public class Supplier : BaseFullAuditedEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}