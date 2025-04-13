namespace KKBookstore.Users;

public static class UserHelper
{
    public static void ConvertFullNameToFirstAndLastName(string fullName, out string firstName, out string lastName)
    {
        var nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        firstName = nameParts.Length > 0 ? nameParts[^1] : "";
        lastName = nameParts.Length > 1 ? string.Join(" ", nameParts[..^1]) : "";
    }
}

