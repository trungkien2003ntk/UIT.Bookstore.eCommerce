using KKBookstore.Models;
using KKBookstore.Users;

namespace KKBookstore.Suppliers;

public class Supplier : BaseFullAuditedEntity, IActivatable
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? WebsiteUrl { get; set; }
    public Address Address { get; set; } = null!;
    public bool IsActive { get; set; }
    public string? Note { get; set; }

    // Tax and banking info
    public string TaxCode { get; set; } = null!;
    public string BankName { get; set; } = null!;
    public string BankAccoutNumber { get; set; } = null!;

    public Supplier(
        string code,
        string name,
        string email,
        string phoneNumber,
        string? websiteUrl,
        Address address,
        bool isActive,
        string taxCode,
        string bankName,
        string bankAccoutNumber,
        string? note = null) : base()
    {
        Code = code;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        WebsiteUrl = websiteUrl;
        Address = address;
        IsActive = isActive;
        TaxCode = taxCode;
        BankName = bankName;
        BankAccoutNumber = bankAccoutNumber;
        Note = note;
    }

    protected Supplier() : base()
    {

    }
}