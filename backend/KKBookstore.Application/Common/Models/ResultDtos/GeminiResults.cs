namespace KKBookstore.Common.Models.ResultDtos;

public class GeminiTextResult
{
    public bool Success { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
    public int? TokenCount { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public class GeminiEmbeddingResult
{
    public bool Success { get; set; }
    public IEnumerable<float>? Values { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public class GeminiTokenCountResult
{
    public bool Success { get; set; }
    public int TotalTokens { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
