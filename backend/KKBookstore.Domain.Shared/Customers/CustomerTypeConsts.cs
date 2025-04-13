namespace KKBookstore.Customers;

public static class CustomerTypeConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int NameMaxLength = 256;
}