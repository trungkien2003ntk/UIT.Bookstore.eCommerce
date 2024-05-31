using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.Products.Models;

public record ProductDetailDto : BaseDto
{
    public string Name { get; set; }
    public decimal MinUnitPrice { get; set; }
    public decimal MaxUnitPrice { get; set; }
    public decimal MinRecommendedRetailPrice { get; set; }
    public decimal MaxRecommendedRetailPrice { get; set; }
    public string UnitMeasureName { get; set; }
    public string Description { get; set; }
    public string ProductTypeName { get; set; }
    public bool IsBook { get; set; }
    public IEnumerable<string> ThumbnailImageUrls { get; set; } = [];
    public IEnumerable<string> LargeImageUrls { get; set; } = [];
    public IEnumerable<AuthorDto>? Authors { get; set; }
    public IEnumerable<SkuDto> Skus { get; set; } = [];
    // other properties as needed
}
