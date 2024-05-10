using KKBookstore.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Extensions;

public static class ResultExtension
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Can't convert success result to problem");
        }

        return Results.Problem(
            statusCode: GetStatusCode(result.Error.ErrorType),
            title: "Bad Request",
            type: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            extensions: new Dictionary<string, object?>
            {
                { "errors", new[] { result.Error } }
            });
    }

    public static IActionResult ToActionResult(this Result result)
    {
        var problemDetails = result.ToProblemDetails();
        return new ObjectResult(problemDetails)
        {
            StatusCode = GetStatusCode(result.Error.ErrorType)
        };
    }

    static int GetStatusCode(ErrorType errorType)
    {
        switch (errorType)
        {
            // switch errorType:
            case ErrorType.Validation:
                return StatusCodes.Status400BadRequest;
            case ErrorType.Unauthorized:
                return StatusCodes.Status401Unauthorized;
            case ErrorType.NotFound:
                return StatusCodes.Status404NotFound;
            case ErrorType.Conflict:
                return StatusCodes.Status409Conflict;
            default:
                return StatusCodes.Status500InternalServerError;
        }
    }
}
