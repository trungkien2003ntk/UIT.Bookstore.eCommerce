using KKBookstore.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace KKBookstore.Domain.Users;

public class User : IdentityUser, ISoftDelete
{
    public string FullName { get; set; }

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
}
