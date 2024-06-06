using KKBookstore.Domain.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Abstractions;

[ApiController]
public abstract class ApiController(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;

    [NonAction]
    protected IActionResult ToActionResult(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                CreateResult(
                    result.Error,
                    validationResult.Errors),
            _ =>
                CreateResult(
                    result.Error)
        };

    private ObjectResult CreateResult(Error error, Error[]? errors = null)
    {
        var errorResponse = new
        {
            errors = errors?.ToArray()
        };

        return new ObjectResult(new
        {
            title = GetTitle(error.ErrorType),
            type = GetType(error.ErrorType),
            code = GetStatusCode(error.ErrorType),
            detail = error.Description,
            extensions = errorResponse
        })
        {
            StatusCode = GetStatusCode(error.ErrorType)
        };
    }

    private int GetStatusCode(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation or ErrorType.BusinessRuleViolation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.ServiceUnavailable => StatusCodes.Status503ServiceUnavailable,
            _ => StatusCodes.Status500InternalServerError,
        };
    }

    private string GetTitle(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation or ErrorType.BusinessRuleViolation => "Bad Request",
            ErrorType.Unauthorized => "Unauthorized",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            ErrorType.Forbidden => "Forbidden",
            ErrorType.ServiceUnavailable => "Service Unavailable",
            _ => "Internal Server Error",
        };
    }

    private string GetType(ErrorType errorType)
    {
        return errorType switch
        {
            ErrorType.Validation or ErrorType.BusinessRuleViolation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.Unauthorized => "https://tools.ietf.org/html/rfc7235#section-3.1",
            ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            ErrorType.Forbidden => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            ErrorType.ServiceUnavailable => "https://tools.ietf.org/html/rfc7231#section-6.6.4",
            _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1",
        };
    }
}
