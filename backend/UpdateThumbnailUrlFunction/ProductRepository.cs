using Microsoft.Data.SqlClient;

namespace UpdateThumbnailUrlFunction;

public interface IProductRepository
{
    Task UpdateThumbnailImageUrlsAsync(int productId, IEnumerable<(int ImageId, string ThumbnailImageUrl)> images);
}

public class ProductRepository : IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository()
    {
        _connectionString = Environment.GetEnvironmentVariable("SqlConnectionString")!;
    }

    public async Task UpdateThumbnailImageUrlsAsync(int productId, IEnumerable<(int ImageId, string ThumbnailImageUrl)> images)
    {
        var commandText = @"
            UPDATE ProductImages
            SET ThumbnailImageUrl = @thumbnailImageUrl
            WHERE ProductId = @productId AND Id = @imageId";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        foreach (var (imageId, thumbnailImageUrl) in images)
        {
            await using var command = new SqlCommand(commandText, connection);
            command.Parameters.AddWithValue("@thumbnailImageUrl", thumbnailImageUrl);
            command.Parameters.AddWithValue("@productId", productId);
            command.Parameters.AddWithValue("@imageId", imageId);

            await command.ExecuteNonQueryAsync();
        }

        await connection.CloseAsync();
    }
}