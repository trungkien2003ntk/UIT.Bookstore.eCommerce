namespace KKBookstore.Domain.Shared.Branches;

public static class BranchConsts
{
    private const string DefaultSorting = "Name asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int NameMaxLength = 256;
    public const int PhoneNumberMaxLength = 13;
    public const int EmailMaxLength = 128;
    public const int DescriptionMaxLength = 1024;
}