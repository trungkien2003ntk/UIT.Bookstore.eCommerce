namespace KKBookstore.Domain.Products.Events;

public record ProductCreatedEvent(int ProductId, IEnumerable<ImageDto> Images);

public record ImageDto(int ImageId, string ThumbnailImageUrl);

