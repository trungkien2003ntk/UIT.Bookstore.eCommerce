using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Products.GetTrendyProductList;

public record GetTrendyProductListResponse
{
    public List<ProductSummary> Items { get; init; }

    public record ProductSummary : BaseDto
    {
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public bool IsBook { get; set; }
        public int SoldCount { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public decimal MinUnitPrice { get; set; }
        public decimal MinRecommendedRetailPrice { get; set; }
        public decimal MinDiscountRate => (MinRecommendedRetailPrice - MinUnitPrice) / MinRecommendedRetailPrice * 100;
        public decimal AverageRating { get; set; }
        public bool IsActive { get; set; }
    }
}
