using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestGeminiController : ControllerBase
{
    private readonly IGeminiService _geminiService;
    private readonly ILogger<TestGeminiController> _logger;

    public TestGeminiController(IGeminiService geminiService, ILogger<TestGeminiController> logger)
    {
        _geminiService = geminiService;
        _logger = logger;
    }

    [HttpPost("generate-text")]
    public async Task<IActionResult> GenerateText([FromBody] GeminiRequest request)
    {
        try
        {
            var result = await _geminiService.GenerateTextAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating text with Gemini");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("test-basic")]
    public async Task<IActionResult> TestBasic()
    {
        try
        {
            var request = new GeminiRequest
            {
                Prompt = "Generate a brief description for a fantasy book in our bookstore.",
                MaxOutputTokens = 100,
                Temperature = 0.7f
            };

            var result = await _geminiService.GenerateTextAsync(request);
            return Ok(new { success = true, result });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in basic Gemini test");
            return BadRequest(new { error = ex.Message, success = false });
        }
    }

    [HttpGet("count-tokens/{text}")]
    public async Task<IActionResult> CountTokens(string text)
    {
        try
        {
            var result = await _geminiService.CountTokensAsync(text);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error counting tokens");
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("embeddings")]
    public async Task<IActionResult> GetEmbeddings([FromBody] string text)
    {
        try
        {
            var result = await _geminiService.GetEmbeddingsAsync(text);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting embeddings");
            return BadRequest(new { error = ex.Message });
        }
    }
}
