using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Orders;

namespace KKBookstore.Domain.Users;

public class Address : BaseFullAuditedEntity
{
    public string PhoneNumber { get; set; }

    public int ProvinceId { get; set; }
    public string ProvinceName { get; set; } = null!;

    public int DistrictId { get; set; }
    public string DistrictName { get; set; } = null!;

    public string CommuneCode { get; set; }
    public string CommuneName { get; set; } = null!;

    public string DetailAddress { get; set; } = null!;

    public bool IsDefault { get; set; }

    public AddressType Type { get; set; }

    public Address(
        string phoneNumber,
        int provinceId,
        string province,
        int districtId,
        string district,
        string communeCode,
        string commune,
        string detailAddress,
        bool isDefault,
        AddressType addressType) : base()
    {
        PhoneNumber = phoneNumber;
        ProvinceId = provinceId;
        ProvinceName = province;
        DistrictId = districtId;
        DistrictName = district;
        CommuneCode = communeCode;
        CommuneName = commune;
        DetailAddress = detailAddress;
        IsDefault = isDefault;
        Type = addressType;
    }

    protected Address() : base()
    {
        PhoneNumber = "";
        ProvinceId = 0;
        ProvinceName = "";
        DistrictId = 0;
        DistrictName = "";
        CommuneCode = "";
        CommuneName = "";
        DetailAddress = "";
        IsDefault = false;
        Type = AddressType.Home;
    }
}