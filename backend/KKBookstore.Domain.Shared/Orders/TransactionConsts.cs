namespace KKBookstore.Domain.Shared.Orders;

public static class TransactionConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int BankCodeMaxLength = 20;
    public const int BankTranNoMaxLength = 260;
    public const int CardTypeMaxLength = 25;
    public const int OrderInfoMaxLength = 260;
    public const int ResponseCodeMaxLength = 20;
    public const int TransactionStatusMaxLength = 50;

}