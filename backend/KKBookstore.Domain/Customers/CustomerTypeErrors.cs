using KKBookstore.Models;

namespace KKBookstore.Customers;

public static class CustomerTypeErrors
{
    public static readonly Error NotFound = Error.NotFound("CustomerType.NotFound", "The customer type was not found");
    public static Error DuplicateCustomerTypeName(string name) => Error.Conflict("CustomerType.DuplicateName", $"A customer type with the name '{name}' already exists");
    public static Error InvalidAttributeValue(string attributeName, IEnumerable<string> validValues) => Error.Validation("CustomerType.InvalidAttributeValue", $"Invalid {attributeName}. Valid values are: {string.Join(", ", validValues)}");
    public static Error InvalidAttribute(string message) => Error.Validation("CustomerType.InvalidAttribute", message);
}
