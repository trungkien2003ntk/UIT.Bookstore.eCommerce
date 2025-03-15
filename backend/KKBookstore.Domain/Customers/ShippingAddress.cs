using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Orders;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.Customers;

public class ShippingAddress : Address
{
    public int CustomerId { get; set; }
    public string ReceiverName { get; set; }

    // navigation property
    public Customer? Customer { get; set; }


    public ShippingAddress() : base()
    {
        ReceiverName = string.Empty;
    }

    private ShippingAddress(
        int userId,
        string receiverName,
        string phoneNumber,
        int provinceId,
        string province,
        int districtId,
        string district,
        string communeCode,
        string commune,
        bool isDefault,
        string detailAddress,
        AddressType addressType
    ) : base(phoneNumber, provinceId, province, districtId, district, communeCode, commune, detailAddress, isDefault, addressType)
    {
        CustomerId = userId;
        ReceiverName = receiverName;
        IsDeleted = false;
    }

    // factory method
    public static Result<ShippingAddress> Create(
        int userId,
        string name,
        string phoneNumber,
        int provinceId,
        string province,
        int districtId,
        string district,
        string communeCode,
        string commune,
        bool isDefault = false,
        string detailAddress = "",
        AddressType addressType = AddressType.Home
    )
    {
        // validation
        return new ShippingAddress(userId, name, phoneNumber, provinceId, province, districtId, district, communeCode, commune, isDefault, detailAddress, addressType);
    }

    public void SetAsDefault()
    {
        IsDefault = true;
    }

    public void RemoveAsDefault()
    {
        IsDefault = false;
    }
}
