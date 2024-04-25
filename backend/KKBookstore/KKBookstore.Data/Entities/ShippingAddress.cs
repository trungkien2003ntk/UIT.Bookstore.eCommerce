using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Data.Entities
{

    [Table("ShippingAddress")]
    public class ShippingAddress : BaseEntity, ISoftDelete
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShippingAddressId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Province { get; set; }

        public string District { get; set; }

        public string Ward { get; set; }

        public string Street { get; set; }

        public string AddressType { get; set; }

        public string? Note { get; set; }

        public bool IsDefault { get; set; }

        public User? User { get; set; }
        
        public bool IsDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public DateTimeOffset? DeletedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
