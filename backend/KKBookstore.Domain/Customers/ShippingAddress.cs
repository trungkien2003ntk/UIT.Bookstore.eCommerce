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
        string province,
        string district,
        string commune,
        bool isDefault,
        string detailAddress,
        AddressType addressType
    ) : base(phoneNumber, province, district, commune, detailAddress, isDefault, addressType)
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
        string province,
        string district,
        string commune,
        bool isDefault = false,
        string detailAddress = "",
        AddressType addressType = AddressType.Home
    )
    {
        // validation
        return new ShippingAddress(userId, name, phoneNumber, province, district, commune, isDefault, detailAddress, addressType);
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
