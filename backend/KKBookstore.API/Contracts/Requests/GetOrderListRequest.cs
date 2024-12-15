﻿namespace KKBookstore.API.Contracts.Requests;

public class GetOrderListRequest
{
    public string SortBy { get; set; } = "CreationTime";
    public string SortDirection { get; set; } = "desc";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public string? OrderStatuses { get; set; }
    public string? SearchQuery { get; set; }
}
