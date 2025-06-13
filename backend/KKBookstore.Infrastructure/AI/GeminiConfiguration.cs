namespace KKBookstore.AI;

public class GeminiConfiguration
{
    public string ApiKey { get; set; } = string.Empty;
    public string TextBaseUrl { get; set; } = "https://generativelanguage.googleapis.com/v1/models/gemini-pro";
    public string ImageBaseUrl { get; set; } = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro-vision";
    public string ModelBaseUrl { get; set; } = "https://generativelanguage.googleapis.com/v1beta/models";
    public string EmbeddingBaseUrl { get; set; } = "https://generativelanguage.googleapis.com/v1beta/models";
}
