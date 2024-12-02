namespace KKBookstore.Domain.Shared.Customers;

public static class CustomerConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

}
