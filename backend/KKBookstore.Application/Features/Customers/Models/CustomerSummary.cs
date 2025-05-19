namespace KKBookstore.Features.Customers.Models;

public class CustomerSummary
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public string? CustomerTypeName { get; set; }
    public int? CustomerTypeId { get; set; }
    public DateTimeOffset? CreationTime { get; set; }
}
