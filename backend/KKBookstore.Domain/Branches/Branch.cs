using KKBookstore.Models;

namespace KKBookstore.Branches;

public class Branch : BaseFullAuditedEntity
{
    public Branch()
    {

    }

    public Branch(
        string name,
        string description,
        string email,
        bool isDefault,
        BranchAddress address
    )
    {
        Name = name;
        Description = description;
        Email = email;
        IsDefault = isDefault;
        Address = address;
        IsDeleted = false;
    }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int AddressId { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }

    public BranchAddress Address { get; set; } = null!;
}