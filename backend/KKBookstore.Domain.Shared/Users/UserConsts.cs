namespace KKBookstore.Domain.Shared.Users;

public static class UserConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int FirstNameMaxLength = 125;
    public const int LastNameMaxLength = 125;
    public const int EmailMaxLength = 255;
    public const int PhoneNumberMaxLength = 13;
    public const int PasswordMaxLength = 255;
    public const int AddressMaxLength = 1024;
    public const int AvatarMaxLength = 255;
    public const int RoleMaxLength = 50;
}