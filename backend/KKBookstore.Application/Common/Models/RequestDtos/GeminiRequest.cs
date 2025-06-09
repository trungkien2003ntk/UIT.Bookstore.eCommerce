namespace KKBookstore.Common.Models.RequestDtos;

public class GeminiRequest
{
    public string Prompt { get; set; } = string.Empty;
    public double? Temperature { get; set; }
    public int? MaxOutputTokens { get; set; }
    public double? TopP { get; set; }
    public int? TopK { get; set; }
    public List<string>? StopSequences { get; set; }
    public string? SafetyCategory { get; set; }
    public string? SafetyThreshold { get; set; }
}
