using KKBookstore.Common.Configuration;
using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace KKBookstore.AI;

public class CommentModerationService : ICommentModerationService
{
    private readonly IGeminiService _geminiService;
    private readonly ILogger<CommentModerationService> _logger;
    private readonly ModerationConfiguration _config;

    public CommentModerationService(
        IGeminiService geminiService,
        ILogger<CommentModerationService> logger,
        IOptions<ModerationConfiguration> config)
    {
        _geminiService = geminiService;
        _logger = logger;
        _config = config.Value;
    }

    public async Task<CommentModerationResult> EvaluateCommentAsync(string comment, string language = "vi")
    {
        try
        {
            if (!_config.IsEnabled)
            {
                _logger.LogInformation("AI moderation is disabled, returning safe result");
                return new CommentModerationResult
                {
                    Success = true,
                    BadnessScore = 1,
                    IsViolation = false,
                    Explanation = "AI moderation disabled",
                    Category = "None"
                };
            }

            var prompt = BuildModerationPrompt(comment, language);

            var geminiRequest = new GeminiRequest
            {
                Prompt = prompt,
                Temperature = _config.AiTemperature,
                MaxOutputTokens = _config.MaxTokens
            };

            _logger.LogInformation("Evaluating comment with AI moderation. Language: {Language}, Length: {Length}",
                language, comment.Length);

            var aiResponse = await _geminiService.GenerateTextAsync(geminiRequest);

            if (!aiResponse.Success)
            {
                _logger.LogError("AI moderation failed: {Error}", aiResponse.ErrorMessage);
                return new CommentModerationResult
                {
                    Success = false,
                    ErrorMessage = aiResponse.ErrorMessage ?? "AI service unavailable"
                };
            }

            var result = ParseAiResponse(aiResponse.Text);
            result.Success = true;

            _logger.LogInformation("AI moderation completed. Score: {Score}, IsViolation: {IsViolation}, Category: {Category}",
                result.BadnessScore, result.IsViolation, result.Category);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during comment moderation");
            return new CommentModerationResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }

    private string BuildModerationPrompt(string comment, string language)
    {
        var policyText = language.ToLower() switch
        {
            "en" => GetEnglishContentPolicy(),
            "vi" => GetVietnameseContentPolicy(),
            _ => GetVietnameseContentPolicy() // Default to Vietnamese
        };

        return $@"You are a content moderation AI for an e-commerce book review system. 

CONTENT POLICY:
{policyText}

Please evaluate the following comment and provide a JSON response with this exact structure:
{{
    ""badnessScore"": <number 1-100>,
    ""isViolation"": <true/false>,
    ""category"": ""<violation category or 'None'>"",
    ""explanation"": ""<one-sentence explanation in {(language == "vi" ? "Vietnamese" : "English")}>"" 
}}

SCORING GUIDE:
- 1-30: Acceptable content
- 31-50: Borderline content (may need review)
- 51-70: Problematic content (likely violation)
- 71-100: Clear violation (auto-hide recommended)

If you are uncertain, select the score and category that most closely matches the content. Do not include any extra commentary—respond ONLY with the JSON object, no other text.

COMMENT TO EVALUATE:
""{comment}""
";
    }

    private string GetVietnameseContentPolicy()
    {
        return @"CHÍNH SÁCH NỘI DUNG:
1. KHÔNG được sử dụng ngôn từ thô tục, chửi bởi, xúc phạm
2. KHÔNG được spam hoặc quảng cáo sản phẩm khác
3. KHÔNG được đe dọa, quấy rối người khác
4. KHÔNG được chia sẻ thông tin cá nhân
5. KHÔNG được nội dung phân biệt chủng tộc, tôn giáo
6. KHÔNG được nội dung khiêu dâm hoặc bạo lực
7. KHÔNG được đánh giá giả mạo hoặc không trung thực
8. NÊN viết đánh giá có tính xây dựng về sản phẩm";
    }

    private string GetEnglishContentPolicy()
    {
        return @"CONTENT POLICY:
1. NO profanity, vulgar language, or personal attacks
2. NO spam or advertising other products
3. NO threats or harassment of others
4. NO sharing of personal information
5. NO discriminatory content based on race, religion, etc.
6. NO adult content or violence
7. NO fake or dishonest reviews
8. SHOULD provide constructive feedback about products";
    }

    private CommentModerationResult ParseAiResponse(string response)
    {
        try
        {
            // Clean the response - remove any markdown formatting or extra text
            var jsonStart = response.IndexOf('{');
            var jsonEnd = response.LastIndexOf('}');

            if (jsonStart >= 0 && jsonEnd > jsonStart)
            {
                var jsonText = response.Substring(jsonStart, jsonEnd - jsonStart + 1);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var parsed = JsonSerializer.Deserialize<JsonElement>(jsonText, options);

                var badnessScore = parsed.GetProperty("badnessScore").GetInt32();
                var isViolation = parsed.GetProperty("isViolation").GetBoolean();
                var category = parsed.GetProperty("category").GetString() ?? "Unknown";
                var explanation = parsed.GetProperty("explanation").GetString() ?? "No explanation provided";

                // Ensure score is within valid range
                badnessScore = Math.Max(1, Math.Min(100, badnessScore));

                return new CommentModerationResult
                {
                    BadnessScore = badnessScore,
                    IsViolation = isViolation || badnessScore >= _config.AutoHideThreshold,
                    Category = category,
                    Explanation = explanation
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to parse AI response: {Response}", response);
        }

        // Fallback: if we can't parse the response, be conservative
        return new CommentModerationResult
        {
            BadnessScore = 50,
            IsViolation = false,
            Category = "ParseError",
            Explanation = "Unable to parse AI response"
        };
    }
}
