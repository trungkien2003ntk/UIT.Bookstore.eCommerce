using System.ComponentModel.DataAnnotations.Schema;
using KKBookstore.Domain.Common.Interfaces;
using KKBookstore.Domain.Users;

namespace KKBookstore.Domain.OrderAggregate;


//ShippingAddressId
//UserId
//Name
//PhoneNumber
//Province
//District
//Commune
//DetailAddress
//IsDefault
//AddressTypeCode
//LastEditedBy
//LastEditedWhen



public class ShippingAddress : BaseEntity, ISoftDelete, ITrackable
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ShippingAddressId { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public string Province { get; set; }

    public string District { get; set; }

    public string Commune { get; set; }

    public string DetailAddress { get; set; }

    public bool IsDefault { get; set; }

    public string AddressTypeCode { get; set; }

    public int LastEditedBy { get; set; }

    public DateTimeOffset LastEditedWhen { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset? DeletedWhen { get; set; }

    // navigation property
    public User LastEditedByUser { get; set; }
    public User User { get; set; }
    public RefAddressType AddressType { get; set; }
}
