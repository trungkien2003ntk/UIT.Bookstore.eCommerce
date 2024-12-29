namespace KKBookstore.Domain.Products.Events;

public record ProductCreatedEvent(int ProductId, IEnumerable<string> ImageUrls);