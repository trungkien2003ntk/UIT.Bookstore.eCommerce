using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Common.Interfaces;

public interface IGeminiService
{
    /// <summary>
    /// Generate text content using Google Gemini AI
    /// </summary>
    /// <param name="prompt">The text prompt to send to Gemini</param>
    /// <returns>Generated text response</returns>
    Task<GeminiTextResult> GenerateTextAsync(string prompt);

    /// <summary>
    /// Generate text content using Google Gemini AI with configuration options
    /// </summary>
    /// <param name="request">The Gemini request with prompt and configuration</param>
    /// <returns>Generated text response</returns>
    Task<GeminiTextResult> GenerateTextAsync(GeminiRequest request);

    /// <summary>
    /// Stream text content generation using Google Gemini AI
    /// </summary>
    /// <param name="prompt">The text prompt to send to Gemini</param>
    /// <param name="callback">Callback function to receive streaming text chunks</param>
    /// <returns>Task representing the streaming operation</returns>
    Task StreamTextAsync(string prompt, Action<string> callback);

    /// <summary>
    /// Generate content with image input using Google Gemini AI
    /// </summary>
    /// <param name="prompt">The text prompt</param>
    /// <param name="imageBase64">Base64 encoded image</param>
    /// <param name="mimeType">Image MIME type (e.g., "image/jpeg", "image/png")</param>
    /// <returns>Generated response based on text and image input</returns>
    Task<GeminiTextResult> GenerateWithImageAsync(string prompt, string imageBase64, string mimeType);

    /// <summary>
    /// Get text embeddings using Google Gemini AI
    /// </summary>
    /// <param name="text">Text to get embeddings for</param>
    /// <returns>Embedding vector</returns>
    Task<GeminiEmbeddingResult> GetEmbeddingsAsync(string text);

    /// <summary>
    /// Count tokens in a text prompt
    /// </summary>
    /// <param name="text">Text to count tokens for</param>
    /// <returns>Token count information</returns>
    Task<GeminiTokenCountResult> CountTokensAsync(string text);
}
