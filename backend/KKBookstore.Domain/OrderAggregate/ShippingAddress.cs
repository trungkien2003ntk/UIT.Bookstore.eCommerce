using KKBookstore.Domain.Common;
using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.OrderAggregate;

public class ShippingAddress : BaseAuditableEntity, ISoftDelete
{
    private ShippingAddress(
        int userId,
        string name,
        string phoneNumber,
        string province,
        string district,
        string commune,
        int addressTypeId,
        string detailAddress
    ) : base()
    {
        UserId = userId;
        Name = name;
        PhoneNumber = phoneNumber;
        Province = province;
        District = district;
        Commune = commune;
        DetailAddress = detailAddress;
        AddressTypeId = addressTypeId;
        IsDeleted = false;
    }

    public int UserId { get; set; }

    public string Name { get; set; }

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
        string detailAddress = ""
    )
    {

        return new ShippingAddress(userId, name, phoneNumber, province, district, commune, addressTypeId, detailAddress);
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
