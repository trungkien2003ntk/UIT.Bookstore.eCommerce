namespace UpdateThumbnailUrlFunction;

public class ThumbnailGeneratedEvent
{
    public int ProductId { get; set; }
    public List<ImageDto> Images { get; set; }

    public ThumbnailGeneratedEvent(int productId, IEnumerable<ImageDto> images)
    {
        ProductId = productId;
        Images = images.ToList();
    }
}