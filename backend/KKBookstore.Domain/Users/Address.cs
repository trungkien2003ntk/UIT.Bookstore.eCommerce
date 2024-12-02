using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Orders;

namespace KKBookstore.Domain.Users;

public class Address : BaseFullAuditedEntity
{
    public string PhoneNumber { get; set; }

    public string Province { get; set; } = null!;
    
    public string District { get; set; } = null!;
    
    public string Commune { get; set; } = null!;
    
    public string DetailAddress { get; set; } = null!;
    
    public bool IsDefault { get; set; }

    public AddressType Type { get; set; }

    public Address(string phoneNumber, string province, string district, string commune, string detailAddress, bool isDefault, AddressType addressType) : base()
    {
        PhoneNumber = phoneNumber;
        Province = province;
        District = district;
        Commune = commune;
        DetailAddress = detailAddress;
        IsDefault = isDefault;
        Type = addressType;
    }

    protected Address() : base()
    {
        PhoneNumber = "";
        Province = "";
        District = "";
        Commune = "";
        DetailAddress = "";
        IsDefault = false;
        Type = AddressType.Home;
    }
}