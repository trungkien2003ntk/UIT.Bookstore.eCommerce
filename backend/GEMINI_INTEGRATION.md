# Gemini AI SDK Integration Documentation

This document describes the integration of Google's Gemini AI SDK into the KKBookstore e-commerce backend following clean architecture principles.

## Recent Fixes

### Safety Settings API Issue (June 2025)
**Issue**: Google Gemini API was rejecting requests with error: `"Invalid JSON payload received. Unknown name 'safetySetting': Cannot find field."`

**Root Cause**: The DotnetGeminiSDK was sending individual `SafetySetting` objects, but Google's API expects `safetySettings` (plural) as an array.

**Solution**: 
1. Removed the problematic safety setting parameter from the `TextPrompt` method call
2. Updated model names from deprecated `gemini-pro` to current `gemini-1.5-flash`
3. Updated API endpoints from `v1beta` to `v1` where appropriate

**Result**: Gemini API calls now work correctly without safety setting conflicts.

## Overview

The Gemini SDK has been integrated to provide AI-powered features for the bookstore, including:
- Text generation (book descriptions, recommendations)
- Content embeddings (for semantic search)
- Image processing capabilities
- Token counting for cost estimation

## Architecture

The integration follows the clean architecture pattern:

```
KKBookstore.Application/
├── Common/
│   ├── Interfaces/
│   │   └── IGeminiService.cs           # Service interface
│   └── Models/
│       ├── RequestDtos/
│       │   └── GeminiRequest.cs        # Request DTOs
│       └── ResultDtos/
│           └── GeminiResults.cs        # Response DTOs

KKBookstore.Infrastructure/
├── AI/
│   ├── GeminiService.cs                # Service implementation
│   └── GeminiConfiguration.cs         # Configuration model
└── DependencyInjection.cs             # DI registration

KKBookstore.API/
├── Controllers/
│   └── TestGeminiController.cs         # Test controller
└── appsettings.json                    # Configuration
```

## Configuration

### 1. API Key Setup

Replace the placeholder in `appsettings.json`:

```json
{
  "Gemini": {
    "ApiKey": "YOUR_ACTUAL_GOOGLE_GEMINI_API_KEY",
    "Model": "gemini-1.5-flash",
    "MaxTokens": 1000,
    "Temperature": 0.7
  }
}
```

### 2. Environment Variables (Recommended)

For production, use environment variables:

```bash
export GEMINI__APIKEY="your-actual-api-key"
```

Or in `appsettings.Production.json`:

```json
{
  "Gemini": {
    "ApiKey": "${GEMINI_API_KEY}"
  }
}
```

## Usage Examples

### Basic Text Generation

```csharp
public class BookService
{
    private readonly IGeminiService _geminiService;

    public BookService(IGeminiService geminiService)
    {
        _geminiService = geminiService;
    }

    public async Task<string> GenerateBookDescription(string bookTitle, string author, string genre)
    {
        var request = new GeminiRequest
        {
            Prompt = $"Write an engaging book description for '{bookTitle}' by {author} in the {genre} genre. Make it compelling for potential readers.",
            MaxTokens = 150,
            Temperature = 0.8f
        };

        var result = await _geminiService.GenerateTextAsync(request);
        return result.GeneratedText;
    }
}
```

### Streaming Text Generation

```csharp
public async Task StreamBookRecommendations(string userPreferences)
{
    var request = new GeminiRequest
    {
        Prompt = $"Based on these preferences: {userPreferences}, recommend 5 books with explanations.",
        MaxTokens = 500,
        Temperature = 0.7f
    };

    await _geminiService.GenerateTextStreamAsync(request, (chunk) =>
    {
        Console.Write(chunk); // Stream to client in real-time
    });
}
```

### Image Processing

```csharp
public async Task<string> AnalyzeBookCover(byte[] imageData)
{
    var request = new GeminiRequest
    {
        Prompt = "Analyze this book cover and describe its genre, mood, and potential target audience.",
        ImageData = imageData,
        ImageMimeType = ImageMimeType.Jpeg
    };

    var result = await _geminiService.ProcessImageAsync(request);
    return result.GeneratedText;
}
```

### Embeddings for Semantic Search

```csharp
public async Task<float[]> GetBookEmbeddings(string bookDescription)
{
    var result = await _geminiService.GetEmbeddingsAsync(bookDescription);
    return result.Embeddings;
}
```

### Token Counting

```csharp
public async Task<int> EstimateCost(string content)
{
    var result = await _geminiService.CountTokensAsync(content);
    return result.TokenCount ?? 0;
}
```

## API Endpoints

The `TestGeminiController` provides endpoints for testing:

- `POST /api/testgemini/generate-text` - Generate text from request
- `POST /api/testgemini/test-basic` - Basic functionality test
- `GET /api/testgemini/count-tokens/{text}` - Count tokens in text
- `POST /api/testgemini/embeddings` - Get text embeddings

## Use Cases for Bookstore

### 1. Product Descriptions
- Generate compelling book descriptions from basic metadata
- Create category-specific marketing copy
- Localize descriptions for different markets

### 2. Recommendation Engine
- Generate personalized book recommendations
- Create themed book lists (e.g., "Books like Harry Potter")
- Explain why a book is recommended

### 3. Search Enhancement
- Use embeddings for semantic book search
- Find books by description rather than exact keywords
- Improve search relevance

### 4. Content Moderation
- Analyze user reviews for inappropriate content
- Categorize feedback sentiment
- Generate response templates for customer service

### 5. Inventory Management
- Generate product tags from book covers
- Categorize books automatically
- Create SEO-friendly content

## Error Handling

The service includes comprehensive error handling:

```csharp
try
{
    var result = await _geminiService.GenerateTextAsync(request);
    // Handle success
}
catch (ArgumentException ex)
{
    // Handle invalid request parameters
}
catch (HttpRequestException ex)
{
    // Handle API connectivity issues
}
catch (Exception ex)
{
    // Handle unexpected errors
    _logger.LogError(ex, "Unexpected error in Gemini service");
}
```

## Performance Considerations

1. **Caching**: Consider caching frequently requested content
2. **Rate Limiting**: Implement rate limiting to manage API costs
3. **Async Operations**: Always use async methods to avoid blocking
4. **Token Management**: Monitor token usage for cost control

## Security

1. **API Key Protection**: Never commit API keys to source control
2. **Input Validation**: Validate all user inputs before sending to Gemini
3. **Output Sanitization**: Sanitize AI-generated content before displaying
4. **Access Control**: Restrict AI features to authorized users

## Testing

### Unit Tests

Test the service with mocked dependencies:

```csharp
[Test]
public async Task GenerateTextAsync_ValidRequest_ReturnsResult()
{
    // Arrange
    var mockClient = new Mock<IGeminiClient>();
    var service = new GeminiService(mockClient.Object, /* other dependencies */);
    
    // Act & Assert
    var result = await service.GenerateTextAsync(validRequest);
    Assert.IsNotNull(result);
}
```

### Integration Tests

Use the test controller to verify end-to-end functionality:

```bash
curl -X POST "https://localhost:7001/api/testgemini/test-basic" \
  -H "Content-Type: application/json"
```

## Monitoring

1. **Logging**: All operations are logged with appropriate levels
2. **Metrics**: Monitor token usage, response times, and error rates
3. **Alerting**: Set up alerts for high error rates or cost thresholds

## Cost Management

1. **Token Counting**: Use `CountTokensAsync` to estimate costs
2. **Request Optimization**: Minimize token usage with focused prompts
3. **Usage Tracking**: Monitor API usage and set budgets
4. **Model Selection**: Choose appropriate models for different use cases

## Next Steps

1. **Configure API Key**: Replace placeholder with actual API key
2. **Test Integration**: Use test endpoints to verify functionality
3. **Implement Features**: Add AI features to existing controllers
4. **Monitor Usage**: Set up logging and monitoring
5. **Optimize Performance**: Implement caching and rate limiting

## Support

For issues with the integration:
1. Check logs for detailed error messages
2. Verify API key configuration
3. Ensure network connectivity to Google's APIs
4. Review the DotnetGeminiSDK documentation
