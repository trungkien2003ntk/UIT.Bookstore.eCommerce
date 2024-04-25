
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Data.Entities
{
    [Table("User")]
    public class User : BaseEntity, ISoftDelete
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? PictureUrl { get; set; }

        public int RoleId { get; set; }

        public int TypeLogin { get; set; }

        public bool? IsActive { get; set; }

        public bool IsDeleted { get; set; }
        
        public DateTimeOffset? DeletedAt { get; set; }

        public ICollection<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();
    }
}
