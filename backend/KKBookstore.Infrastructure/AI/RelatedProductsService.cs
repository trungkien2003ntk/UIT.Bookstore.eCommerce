using KKBookstore.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace KKBookstore.AI;

/// <summary>
/// Service for fetching AI-recommended related products from external AI endpoints
/// </summary>
public class RelatedProductsService : IRelatedProductsService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<RelatedProductsService> _logger;
    private const string BaseUrl = "https://trungkien2003ntk-bookstore-ai.hf.space";

    public RelatedProductsService(IHttpClientFactory httpClientFactory, ILogger<RelatedProductsService> logger)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
    }

    /// <summary>
    /// Get related product IDs based on a product ID
    /// </summary>
    /// <param name="productId">The product ID to find related products for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of related product IDs</returns>
    public async Task<List<int>> GetRelatedProductIdsByIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"{BaseUrl}/product/{productId}/related";
            _logger.LogInformation("Fetching related products for product ID {ProductId} from {Endpoint}", productId, endpoint);

            var response = await _httpClient.PostAsync(endpoint, null, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to fetch related products for product ID {ProductId}. Status: {StatusCode}, Response: {Response}",
                    productId, response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
                return [];
            }

            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
            var productIdStrings = JsonSerializer.Deserialize<List<string>>(jsonResponse) ?? [];
            var productIds = productIdStrings.Select(int.Parse)
                .Where(x => x != productId)
                .ToList();

            _logger.LogInformation("Successfully fetched {Count} related products for product ID {ProductId}",
                productIds.Count, productId);

            return productIds;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching related products for product ID {ProductId}", productId);
            return [];
        }
    }

    /// <summary>
    /// Get related product IDs based on a base64-encoded image
    /// </summary>
    /// <param name="base64Image">Base64-encoded image</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of related product IDs</returns>
    public async Task<List<int>> GetRelatedProductIdsByImageAsync(string base64Image, CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"{BaseUrl}/product/related-by-image";
            _logger.LogInformation("Fetching related products by image from {Endpoint}", endpoint);

            var requestPayload = new
            {
                base64_image = base64Image
            };

            var jsonContent = JsonSerializer.Serialize(requestPayload);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to fetch related products by image. Status: {StatusCode}, Response: {Response}",
                    response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
                return [];
            }

            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
            var productIdStrings = JsonSerializer.Deserialize<List<string>>(jsonResponse) ?? [];
            var productIds = productIdStrings.Select(int.Parse).ToList();

            _logger.LogInformation("Successfully fetched {Count} related products by image", productIds.Count);

            return productIds;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching related products by image: {Error}", ex.Message);
            return [];
        }
    }
}
