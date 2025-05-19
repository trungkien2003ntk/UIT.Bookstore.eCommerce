using KKBookstore.Models;

namespace KKBookstore.Products;

public class RatingImage : BaseEntity
{
    public RatingImage()
    {
    }
    public RatingImage(string imageUrl, int ratingId) : base()
    {
        ImageUrl = imageUrl;
        RatingId = ratingId;
    }
    public string ImageUrl { get; set; } = string.Empty;
    public int RatingId { get; set; } = default!;
    // navigation property
    public Rating Rating { get; set; } = default!;
    public static Result<RatingImage> Create(string imageUrl, int ratingId)
    {
        return new RatingImage(imageUrl, ratingId);
    }
}
