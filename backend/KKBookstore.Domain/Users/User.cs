using KKBookstore.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Users;

public class User : IdentityUser<int>, IFullAuditedObject
{
    public User()
    {
        FirstName = "";
        LastName = "";
        FullName = "";
        IsActive = true;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string FullName { get; private set; }

    public DateTimeOffset DateOfBirth { get; set; }

    public string? ImageUrl { get; set; }

    public string? UserPreferences { get; set; }

    public Gender Gender { get; set; }

    public LoginType LoginType { get; set; }

    public SignInSource SignInSource { get; set; }
    public bool IsActive { get; set; }

    public UserStatus Status { get; set; }

    // JWT Token Versioning for force logout
    public Guid TokenVersion { get; set; } = Guid.NewGuid();


    // Auditing
    public bool IsDeleted { get; set; }

    public int? DeleterId { get; set; }

    [NotMapped]
    public User? Deleter { get; set; }

    public DateTimeOffset? DeletionTime { get; set; }

    public DateTimeOffset? CreationTime { get; set; }

    public int? CreatorId { get; set; }

    [NotMapped]
    public User? Creator { get; set; }

    public int? LastModifierId { get; set; }

    [NotMapped]
    public User? LastModifier { get; set; }

    public DateTimeOffset? LastModificationTime { get; set; }
    public void MarkAsBlocked()
    {
        IsActive = false;
        Status = UserStatus.Blocked;
        TokenVersion = Guid.NewGuid(); // Generate new token version to invalidate existing tokens
    }

    public void RegenerateTokenVersion()
    {
        TokenVersion = Guid.NewGuid();
    }
}