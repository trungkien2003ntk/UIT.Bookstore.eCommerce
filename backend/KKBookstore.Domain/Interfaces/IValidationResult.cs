using KKBookstore.Domain.Models;

namespace KKBookstore.Interfaces;

public interface IValidationResult
{
    public static readonly Error ValidationError = Error.Validation("ValidationError", "One or more validation errors occurred.");

    Error[] Errors { get; }
}
