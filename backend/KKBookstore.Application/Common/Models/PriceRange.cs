namespace KKBookstore.Application.Common.Models;

public class PriceRange
{
    public PriceRange(
        int minPrice,
        int maxPrice
    )
    {
        MinPrice = minPrice;
        MaxPrice = maxPrice;
    }
    public int MinPrice { get; set; }
    public int MaxPrice { get; set; }
}
