using KKBookstore.Domain.Models;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Branches;

public class Branch : BaseFullAuditedEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int AddressId { get; set; }
    public bool IsDefault { get; set; }

    public Address Address { get; set; } = null!;
}