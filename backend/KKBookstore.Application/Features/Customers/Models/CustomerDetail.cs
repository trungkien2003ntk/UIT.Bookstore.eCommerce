using KKBookstore.Users;

namespace KKBookstore.Features.Customers.Models;

public class CustomerDetail
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public UserStatus Status { get; set; }
    public int? CustomerTypeId { get; set; }
    public string? CustomerTypeName { get; set; }
    public DateTimeOffset? CreationTime { get; set; }
    public int? CreatorId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
    public int? LastModifierId { get; set; }
    public List<ShippingAddressSummary>? ShippingAddresses { get; set; }
}
