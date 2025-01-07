using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Domain.Products;

namespace KKBookstore.Application.Features.Products.Models;

public sealed record ProductVariantDto : BaseDto
{
    public decimal RecommendedRetailPrice { get; set; }
    public decimal UnitPrice { get; set; }
    public int Weight { get; set; }
    public Dimension Dimension { get; set; }
    public decimal TaxRate { get; set; }
    public string Comment { get; set; }
    public int StockQuantity { get; set; }

    public ICollection<VariantOptionDto> VariantOptions { get; set; } = [];

    public sealed record VariantOptionDto
    {
        public int ProductOptionId { get; set; }
        public int ProductOptionValueId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

