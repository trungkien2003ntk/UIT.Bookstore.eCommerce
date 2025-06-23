namespace KKBookstore.Contracts.Requests.Products;

public record CreateProductRatingRequest(
    string Comment,
    int RatingValue,
    List<string> ImageUrls
);
