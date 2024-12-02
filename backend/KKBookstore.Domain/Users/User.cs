using KKBookstore.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Domain.Users;

public class User : IdentityUser<int>, IFullAuditedObject
{
    public User()
    {
        FirstName = "";
        LastName = "";
        IsActive = true;
        IsEmployee = false;
        IsAdmin = false;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    [NotMapped]
    public string FullName => $"{LastName} {FirstName}";

    public DateTimeOffset DateOfBirth { get; set; }

    public string? ImageUrl { get; set; }

    public string? UserPreferences { get; set; }

    public LoginType LoginType { get; set; }

    public bool IsActive { get; set; }

    public bool IsEmployee { get; set; }

    public bool IsAdmin { get; set; }

    public UserStatus Status { get; set; }
    
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
}