namespace KKBookstore.Application.Features.Products.Models;

public sealed record ProductTypeAttributeProductValueDto
{
    public int AttributeId { get; set; }
    public int AttributeValueId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}