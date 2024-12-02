﻿using System.Xml.Linq;

namespace KKBookstore.Domain.Shared.Orders;

public static class DiscountVoucherConsts
{
    private const string DefaultSorting = "CreationTime asc";
    public static string GetDefaultSorting() => DefaultSorting;

    public const int NameMaxLength = 125;
    public const int CodeMaxLength = 50;
    public const int DescriptionMaxLength = 1024;
}