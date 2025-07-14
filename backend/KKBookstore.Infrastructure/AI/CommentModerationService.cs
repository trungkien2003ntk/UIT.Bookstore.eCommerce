using KKBookstore.Common.Configuration;
using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Products;
using KKBookstore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace KKBookstore.AI;

public class CommentModerationService : ICommentModerationService
{
    private readonly IGeminiService _geminiService;
    private readonly ILogger<CommentModerationService> _logger;
    private readonly ModerationConfiguration _config;
    private readonly IApplicationDbContext _dbContext;

    public CommentModerationService(
        IGeminiService geminiService,
        ILogger<CommentModerationService> logger,
        IOptions<ModerationConfiguration> config,
        IApplicationDbContext dbContext)
    {
        _geminiService = geminiService;
        _logger = logger;
        _config = config.Value;
        _dbContext = dbContext;
    }

    public async Task<CommentModerationResult> EvaluateCommentAsync(string comment, string language = "vi")
    {
        try
        {
            var currentLevel = await _dbContext.Settings
                .FirstOrDefaultAsync(x => x.Key == ApplicationSettingKeys.CurrentModerationLevel);

            var currLevelEnum = Enum.Parse<ModerationLevel>(currentLevel!.Value);

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

            var currentLevelSettings = _config.GetCurrentLevelSettings(currentLevel.Value);
            var prompt = BuildModerationPrompt(comment, language);

            var geminiRequest = new GeminiRequest
            {
                Prompt = prompt,
                Temperature = currentLevelSettings.AiTemperature,
                MaxOutputTokens = currentLevelSettings.MaxTokens
            };

            _logger.LogInformation("Evaluating comment with AI moderation. Level: {Level}, Language: {Language}, Length: {Length}",
                currLevelEnum, language, comment.Length);

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

            var result = ParseAiResponse(aiResponse.Text, currentLevelSettings.AutoHideThreshold);
            result.Success = true;

            _logger.LogInformation("AI moderation completed. Level: {Level}, Score: {Score}, IsViolation: {IsViolation}, Category: {Category}",
                currLevelEnum, result.BadnessScore, result.IsViolation, result.Category);

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

    public async Task<CommentModerationResult> EvaluateCommentWithContextAsync(string comment, int productId, string language = "vi")
    {
        try
        {
            var currentLevel = await _dbContext.Settings
                .FirstOrDefaultAsync(x => x.Key == ApplicationSettingKeys.CurrentModerationLevel);

            var currLevelEnum = Enum.Parse<ModerationLevel>(currentLevel!.Value);

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

            // Get product context
            var product = await _dbContext.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id == productId);

            var currentLevelSettings = _config.GetCurrentLevelSettings(currentLevel.Value);
            var prompt = BuildContextAwareModerationPrompt(comment, product, language);

            var geminiRequest = new GeminiRequest
            {
                Prompt = prompt,
                Temperature = currentLevelSettings.AiTemperature,
                MaxOutputTokens = currentLevelSettings.MaxTokens
            };

            _logger.LogInformation("Evaluating comment with context-aware AI moderation. ProductId: {ProductId}, Level: {Level}, Language: {Language}",
                productId, currLevelEnum, language);

            var aiResponse = await _geminiService.GenerateTextAsync(geminiRequest);

            if (!aiResponse.Success)
            {
                _logger.LogError("Context-aware AI moderation failed: {Error}", aiResponse.ErrorMessage);
                // Fallback to regular moderation
                return await EvaluateCommentAsync(comment, language);
            }

            var result = ParseAiResponse(aiResponse.Text, currentLevelSettings.AutoHideThreshold);
            result.Success = true;

            _logger.LogInformation("Context-aware AI moderation completed. ProductId: {ProductId}, Score: {Score}, IsViolation: {IsViolation}",
                productId, result.BadnessScore, result.IsViolation);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during context-aware comment moderation");
            // Fallback to regular moderation
            return await EvaluateCommentAsync(comment, language);
        }
    }

    private string BuildModerationPrompt(string comment, string language)
    {
        language ??= "vi";
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

    private string BuildContextAwareModerationPrompt(string comment, Product? product, string language)
    {
        language ??= "vi";
        var policyText = language.ToLower() switch
        {
            "en" => GetEnglishContentPolicy(),
            "vi" => GetVietnameseContentPolicy(),
            _ => GetVietnameseContentPolicy()
        };

        var productContext = "";
        if (product != null)
        {
            productContext = language.ToLower() == "vi"
                ? $@"
THÔNG TIN SẢN PHẨM:
- Tên sản phẩm: {product.Name}
- Danh mục: {product.ProductType?.DisplayName ?? "Không rõ"}
- Mô tả: {(string.IsNullOrEmpty(product.Description) ? "Không có mô tả" : product.Description.Substring(0, Math.Min(200, product.Description.Length)))}

Hãy xem xét thông tin sản phẩm khi đánh giá bình luận để hiểu ngữ cảnh phù hợp."
                : $@"
PRODUCT CONTEXT:
- Product Name: {product.Name}
- Category: {product.ProductType?.DisplayName ?? "Unknown"}
- Description: {(string.IsNullOrEmpty(product.Description) ? "No description" : product.Description.Substring(0, Math.Min(200, product.Description.Length)))}

Consider this product information when evaluating the comment to understand the appropriate context.";
        }

        return $@"You are a content moderation AI for an e-commerce book review system. 

CONTENT POLICY:
{policyText}
{productContext}

Please evaluate the following comment and provide a JSON response with this exact structure:
{{
    ""badnessScore"": <number 1-100>,
    ""isViolation"": <true/false>,
    ""category"": ""<violation category or 'None'>"",
    ""explanation"": ""<one-sentence explanation in {(language == "vi" ? "Vietnamese" : "English")}>"",
    ""sentimentScore"": <number -1.0 to 1.0>,
    ""sentimentLabel"": ""<Positive/Negative/Neutral>""
}}

CONTEXT-BASED EVALUATION RULES:
- WITH PRODUCT CONTEXT: Evaluate if criticism relates to actual product content. Valid criticism of controversial/questionable content should have LOWER badnessScore.
- WITHOUT PRODUCT CONTEXT: Aggressive accusations without evidence should have HIGHER badnessScore.

SCORING GUIDE:
- 1-30: Acceptable content
- 31-50: Borderline content (may need review)
- 51-70: Problematic content (likely violation)
- 71-100: Clear violation (auto-hide recommended)

EVALUATION PRIORITY:
1. Check for clear policy violations (profanity, threats, spam)
2. If product context provided: Assess if criticism is relevant to actual product features
3. If no context: Evaluate based on tone, evidence, and potential to mislead

SENTIMENT GUIDE:
- sentimentScore: -1.0 (very negative) to 1.0 (very positive)
- sentimentLabel: Positive (>0.1), Negative (<-0.1), Neutral (-0.1 to 0.1)

If you are uncertain, select the score and category that most closely matches the content. Do not include any extra commentary—respond ONLY with the JSON object, no other text.

COMMENT TO EVALUATE:
""{comment}""
";
    }

    private CommentModerationResult ParseAiResponse(string response, int autoHideThreshold)
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

                // Try to get sentiment data (optional for backward compatibility)
                decimal? sentimentScore = null;
                string? sentimentLabel = null;

                if (parsed.TryGetProperty("sentimentScore", out var sentimentElement))
                {
                    sentimentScore = sentimentElement.GetDecimal();
                }

                if (parsed.TryGetProperty("sentimentLabel", out var labelElement))
                {
                    sentimentLabel = labelElement.GetString();
                }

                // Ensure score is within valid range
                badnessScore = Math.Max(1, Math.Min(100, badnessScore));

                if (sentimentScore.HasValue)
                {
                    sentimentScore = Math.Max(-1.0m, Math.Min(1.0m, sentimentScore.Value));
                }

                return new CommentModerationResult
                {
                    BadnessScore = badnessScore,
                    IsViolation = isViolation || badnessScore >= autoHideThreshold,
                    Category = category,
                    Explanation = explanation,
                    SentimentScore = sentimentScore,
                    SentimentLabel = sentimentLabel
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
