using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace KKBookstore.API.Infrastructure;



public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        // try to handle expected exceptions here

        var problemDetails = new ProblemDetails
        {
            Status = GetStatusCode(exception),
            Title = GetTitle(exception),
            Type = GetHelpLink(exception),
            Detail = GetDetail(exception)
        };

        httpContext.Response.StatusCode = GetStatusCode(exception);

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private static string GetDetail(Exception exception)
    {
        var errors = GetErrors(exception);
        return JsonSerializer.Serialize(errors);
    }

    private static int GetStatusCode(Exception exception) =>
       exception switch
       {
           ValidationException => StatusCodes.Status400BadRequest,
           _ => StatusCodes.Status500InternalServerError
       };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ValidationException => "One or more validation errors occurred.",
            ApplicationException applicationException => applicationException.Source ?? "Server Error",
            _ => "Server Error"
        };

    private static string GetHelpLink(Exception exception) =>
        exception switch
        {
            ValidationException => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ApplicationException applicationException => applicationException.HelpLink ?? "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };

    private static IReadOnlyDictionary<string, string[]> GetErrors(Exception exception)
    {
        IReadOnlyDictionary<string, string[]> errors = new Dictionary<string, string[]>();
        if (exception is ValidationException validationException)
        {
            errors = validationException.Errors.ToDictionary(x => x.PropertyName, x => new[] { x.ErrorMessage });
        }
        return errors;
    }
}

