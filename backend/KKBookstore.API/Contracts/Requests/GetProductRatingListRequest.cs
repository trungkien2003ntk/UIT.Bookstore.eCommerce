namespace KKBookstore.API.Contracts.Requests;

public class GetProductRatingListRequest
{
    public int? ProductId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public string Statuses { get; set; } = "Approved";
}
