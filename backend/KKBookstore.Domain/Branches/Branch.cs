using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Branches;

public class Branch : BaseFullAuditedEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int AddressId { get; set; }
    public bool IsDefault { get; set; }

    public BranchAddress Address { get; set; } = null!;
}