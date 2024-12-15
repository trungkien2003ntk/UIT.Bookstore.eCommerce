using KKBookstore.API.HelperModels;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Contracts.Requests;

public class GetProductListRequest
{
    public string SortBy { get; set; } = "CreationTime";
    public string SortDirection { get; set; } = "desc";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 12;

    [ModelBinder(typeof(CommaDelimitedArrayModelBinder<int>))]
    public List<int>? ProductTypeIds { get; set; }

    [ModelBinder(typeof(CommaDelimitedArrayModelBinder<int>))]
    public List<int>? ExcludeProductIds { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    // sending this in the url by using: ?CustomFilters[Key]=Value, if you pass key or value in utf8, before sending it, you should encode it to base64
    public Dictionary<string, string>? CustomFilters { get; set; }
    public string? SearchQuery { get; set; }
}
