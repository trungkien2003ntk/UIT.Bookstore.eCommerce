using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Products.SearchProducts;

public record SearchProductsResponse
{
    public int TotalCount { get; init; }
    public List<ProductDto> Products { get; init; }
    public int TotalPages { get; init; }
    public int PageSize { get; init; }
    public int PageNumber { get; init; }

    public sealed record ProductDto : BaseDto
    {
        public string Name { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string Description { get; set; }
        public bool IsBook { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public decimal MinUnitPrice { get; set; }
        public decimal MinRecommendedRetailPrice { get; set; }
        public decimal MinDiscountRate => (MinRecommendedRetailPrice - MinUnitPrice) / MinRecommendedRetailPrice * 100;
        public decimal AverageRating { get; set; }
        public bool IsActive { get; set; }
    }
}