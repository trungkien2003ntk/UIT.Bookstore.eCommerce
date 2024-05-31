using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Aggregates.UserAggregate;

public class ShippingAddress : BaseAuditableEntity, ISoftDelete
{
    public ShippingAddress()
    {

    }
    private ShippingAddress(
        int userId,
        string receiverName,
        string phoneNumber,
        string province,
        string district,
        string commune,
        int addressTypeId,
        bool isDefault,
        string detailAddress
    ) : base()
    {
        UserId = userId;
        ReceiverName = receiverName;
        PhoneNumber = phoneNumber;
        Province = province;
        District = district;
        Commune = commune;
        DetailAddress = detailAddress;
        IsDefault = isDefault;
        AddressTypeId = addressTypeId;
        IsDeleted = false;
    }

    public int UserId { get; set; }
    public string ReceiverName { get; set; }
    public string PhoneNumber { get; set; }
    public string Province { get; set; }
    public string District { get; set; }
    public string Commune { get; set; }
    public string DetailAddress { get; set; }
    public bool IsDefault { get; set; }
    public int AddressTypeId { get; set; }
    public AddressType AddressTypeEnum
    {
        get => (AddressType)AddressTypeId;
        set => AddressTypeId = (int)value;
    }
    public bool IsDeleted { get; set; }

    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation property
    public User User { get; set; }
    public RefAddressType AddressType { get; set; }

    // factory method
    public static Result<ShippingAddress> Create(
        int userId,
        string name,
        string phoneNumber,
        string province,
        string district,
        string commune,
        int addressTypeId,
        bool isDefault = false,
        string detailAddress = ""
    )
    {

        return new ShippingAddress(userId, name, phoneNumber, province, district, commune, addressTypeId, isDefault, detailAddress);
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
