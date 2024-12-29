namespace ProcessImageResizeFunction;

public class ProductCreatedEvent
{
    public int ProductId { get; set; }
    public List<ImageDto> Images { get; set; }

    public ProductCreatedEvent(int productId, IEnumerable<ImageDto> images)
    {
        ProductId = productId;
        Images = images.ToList();
    }
}