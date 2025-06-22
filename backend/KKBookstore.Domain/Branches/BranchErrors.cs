using KKBookstore.Models;

namespace KKBookstore.Branches;

public static class BranchErrors
{
    public static readonly Error NotFound = Error.NotFound("Branch.NotFound", "The branch was not found");
    public static readonly Error DefaultBranchExists = Error.Conflict("Branch.DefaultBranchExists", "A default branch already exists");
    public static readonly Error CannotDeleteDefault = Error.Conflict("Branch.CannotDeleteDefault", "Cannot delete the default branch");
    public static readonly Error AlreadyDeleted = Error.Validation("Branch.AlreadyDeleted", "The branch has already been deleted");
    public static readonly Error CannotDeleteWithFulfillmentsOrInventoriesOrTransactions = Error.Validation("Branch.CannotDeleteWithFulfillmentsOrTransactions", "Cannot delete the branch because it has associated fulfillments, inventories or stock transactions. Please disable the branch instead.");
    public static Error DuplicateBranchName(string name) => Error.Conflict("Branch.DuplicateName", $"A branch with the name '{name}' already exists");
    public static Error InvalidAttributeValue(string attributeName, IEnumerable<string> validValues) => Error.Validation("Branch.InvalidAttributeValue", $"Invalid {attributeName}. Valid values are: {string.Join(", ", validValues)}");
    public static Error InvalidAttribute(string message) => Error.Validation("Branch.InvalidAttribute", message);
}