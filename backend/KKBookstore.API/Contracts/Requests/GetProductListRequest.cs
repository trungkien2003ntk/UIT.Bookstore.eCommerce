using KKBookstore.HelperModels;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Contracts.Requests;

public class GetProductListRequest
{
    public string SortBy { get; set; } = "CreationTime";
    public string SortDirection { get; set; } = "desc";
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public List<int>? ProductTypeIds { get; set; }

    [ModelBinder(typeof(CommaDelimitedArrayModelBinder<int>))]
    public List<int>? ExcludeProductIds { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public Dictionary<string, List<string>> CustomFilters { get; set; } = [];
    public bool IsActive { get; set; } = true;
    public string? SearchQuery { get; set; }
    public int? WarehouseId { get; set; }
}
