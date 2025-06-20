namespace KKBookstore.Features.Products.Models;

public class RatingDto
{
    public int Id { get; set; }
    public string Comment { get; set; }
    public int RatingValue { get; set; }
    public int? CustomerId { get; set; }
    public string CustomerName { get; set; }
    public DateTimeOffset CreationTime { get; set; }
    public int ProductVariantId { get; set; }
    public int LikesCount { get; set; }
    public string Response { get; set; }
    public bool IsReported { get; set; }
    public List<ProductVariantOptionDto> VariantOptions { get; set; } = new();
}

public class ProductVariantOptionDto
{
    public int ProductOptionId { get; set; }
    public int ProductOptionValueId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}
