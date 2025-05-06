using KKBookstore.Models;
using System.Collections.Generic;

namespace KKBookstore.Features.Products.GetWeeklyTopSellingProductList;

public class GetWeeklyTopSellingProductListResponse
{
    public ICollection<ProductSummary> Items { get; set; } = new List<ProductSummary>();

    public class ProductSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public bool IsBook { get; set; }
        public int SoldCount { get; set; }
        public decimal MinUnitPrice { get; set; }
        public decimal MinRecommendedRetailPrice { get; set; }
        public decimal AverageRating { get; set; }
        public bool IsActive { get; set; }
        public string ThumbnailImageUrl { get; set; }
    }
}