namespace KKBookstore.API.Contracts.Requests;

public class GetProductListRequest
{
    public string SortBy { get; set; } = "CreatedWhen";
    public string SortDirection { get; set; } = "desc";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public string? ProductTypeIds { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    // sending this in the url by using: ?CustomFilters[Key]=Value, if you pass key or value in utf8, before sending it, you should encode it to base64
    public Dictionary<string, string>? CustomFilters { get; set; }
    public string? ExcludeProductIds { get; set; }
    public string? Search { get; set; }
}
