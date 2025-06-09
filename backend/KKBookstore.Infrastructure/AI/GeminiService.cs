using DotnetGeminiSDK.Client.Interfaces;
using DotnetGeminiSDK.Model;
using DotnetGeminiSDK.Model.Request;
using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Infrastructure.AI;

public class GeminiService : IGeminiService
{
    private readonly IGeminiClient _geminiClient;
    private readonly ILogger<GeminiService> _logger;

    public GeminiService(IGeminiClient geminiClient, ILogger<GeminiService> logger)
    {
        _geminiClient = geminiClient;
        _logger = logger;
    }

    public async Task<GeminiTextResult> GenerateTextAsync(string prompt)
    {
        try
        {
            _logger.LogInformation("Generating text with Gemini AI for prompt: {Prompt}", prompt.Substring(0, Math.Min(50, prompt.Length)));            var response = await _geminiClient.TextPrompt(prompt);

            if (response?.Candidates?.Any() == true)
            {
                var generatedText = response.Candidates.First().Content?.Parts?.FirstOrDefault()?.Text ?? string.Empty;
                
                return new GeminiTextResult
                {
                    Success = true,
                    Text = generatedText,
                    TokenCount = null // Token count not available in response
                };
            }

            _logger.LogWarning("No candidates returned from Gemini AI");
            return new GeminiTextResult
            {
                Success = false,
                ErrorMessage = "No response generated from Gemini AI"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating text with Gemini AI");
            return new GeminiTextResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<GeminiTextResult> GenerateTextAsync(GeminiRequest request)
    {
        try
        {
            _logger.LogInformation("Generating text with Gemini AI with custom configuration");

            var generationConfig = new GenerationConfig();
            if (request.Temperature.HasValue)
                generationConfig.Temperature = request.Temperature.Value;
            if (request.MaxOutputTokens.HasValue)
                generationConfig.MaxOutputTokens = request.MaxOutputTokens.Value;
            if (request.TopP.HasValue)
                generationConfig.TopP = request.TopP.Value;
            if (request.TopK.HasValue)
                generationConfig.TopK = request.TopK.Value;
            if (request.StopSequences?.Any() == true)
                generationConfig.StopSequences = request.StopSequences;            // Safety settings are handled differently in the SDK to avoid API issues
            // For now, we'll use the overload without safety settings to prevent the
            // "Unknown name 'safetySetting'" error from Google's API
            var response = await _geminiClient.TextPrompt(request.Prompt, generationConfig);

            if (response?.Candidates?.Any() == true)
            {
                var generatedText = response.Candidates.First().Content?.Parts?.FirstOrDefault()?.Text ?? string.Empty;
                
                return new GeminiTextResult
                {
                    Success = true,
                    Text = generatedText,
                    TokenCount = null // Token count not available in response
                };
            }

            _logger.LogWarning("No candidates returned from Gemini AI");
            return new GeminiTextResult
            {
                Success = false,
                ErrorMessage = "No response generated from Gemini AI"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating text with Gemini AI");
            return new GeminiTextResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task StreamTextAsync(string prompt, Action<string> callback)
    {        try
        {
            _logger.LogInformation("Starting text streaming with Gemini AI");
            await _geminiClient.StreamTextPrompt(prompt, (chunk) => callback(chunk ?? string.Empty));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error streaming text with Gemini AI");
            callback($"Error: {ex.Message}");
        }
    }

    public async Task<GeminiTextResult> GenerateWithImageAsync(string prompt, string imageBase64, string mimeType)
    {
        try
        {
            _logger.LogInformation("Generating content with image using Gemini AI");            ImageMimeType imageMimeType = mimeType.ToLower() switch
            {
                "image/jpeg" or "image/jpg" => ImageMimeType.Jpeg,
                "image/png" => ImageMimeType.Png,
                "image/webp" => ImageMimeType.Webp,
                "image/heic" => ImageMimeType.Heic,
                "image/heif" => ImageMimeType.Heif,
                _ => ImageMimeType.Jpeg
            };

            var response = await _geminiClient.ImagePrompt(prompt, imageBase64, imageMimeType);            if (response?.Candidates?.Any() == true)
            {
                var generatedText = response.Candidates.First().Content?.Parts?.FirstOrDefault()?.Text ?? string.Empty;
                
                return new GeminiTextResult
                {
                    Success = true,
                    Text = generatedText,
                    TokenCount = null // Token count not available in response
                };
            }

            _logger.LogWarning("No candidates returned from Gemini AI for image prompt");
            return new GeminiTextResult
            {
                Success = false,
                ErrorMessage = "No response generated from Gemini AI"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating content with image using Gemini AI");
            return new GeminiTextResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<GeminiEmbeddingResult> GetEmbeddingsAsync(string text)
    {
        try
        {
            _logger.LogInformation("Getting embeddings with Gemini AI");

            var response = await _geminiClient.EmbeddedContentsPrompt(text);

            if (response?.Embedding?.Values?.Any() == true)
            {
                var values = response.Embedding.Values.Select(v => Convert.ToSingle(v)).ToList();
                
                return new GeminiEmbeddingResult
                {
                    Success = true,
                    Values = values
                };
            }

            _logger.LogWarning("No embeddings returned from Gemini AI");
            return new GeminiEmbeddingResult
            {
                Success = false,
                ErrorMessage = "No embeddings generated from Gemini AI"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting embeddings with Gemini AI");
            return new GeminiEmbeddingResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    public async Task<GeminiTokenCountResult> CountTokensAsync(string text)
    {
        try
        {
            _logger.LogInformation("Counting tokens with Gemini AI");

            var response = await _geminiClient.CountTokens(text);            if (response != null)
            {
                return new GeminiTokenCountResult
                {
                    Success = true,
                    TotalTokens = (int)response.TotalTokens
                };
            }

            _logger.LogWarning("No token count returned from Gemini AI");
            return new GeminiTokenCountResult
            {
                Success = false,
                ErrorMessage = "No token count returned from Gemini AI"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error counting tokens with Gemini AI");
            return new GeminiTokenCountResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
