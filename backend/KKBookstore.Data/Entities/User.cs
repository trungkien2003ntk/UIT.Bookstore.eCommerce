
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Data.Entities
{
    public class User : BaseEntity, ISoftDelete, ITrackable
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public string? Phone { get; set; }

        public string? ImageUrl { get; set; }

        public string? UserPreferences { get; set; }

        public string LoginType { get; set; }

        public bool IsActive { get; set; }

        public bool IsEmployee { get; set; }

        public bool IsAdmin { get; set; }

        public string Status { get; set; }

        public bool IsDeleted { get; set; }
        
        public DateTimeOffset? DeletedWhen { get; set; }

        public int LastEditedBy { get; set; }

        public DateTimeOffset LastEditedWhen { get; set; }

        // navigation property
        public User LastEditedByUser { get; set; }
        public ICollection<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
