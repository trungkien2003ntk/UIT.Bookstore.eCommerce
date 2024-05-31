namespace KKBookstore.Domain.Models;

public record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new("Error.NullValue", "Null value was provided", ErrorType.Validation);

    public static implicit operator Result(Error error) => Result.Failure(error);

    private Error(string code, string description, ErrorType errorType)
    {
        Code = code;
        Description = description;
        ErrorType = errorType;
    }

    public Result ToResult() => Result.Failure(this);


    public string Code { get; }
    public string Description { get; }
    public ErrorType ErrorType { get; }

    // static factory methods
    public static Error NotFound(string code, string description) => new(code, description, ErrorType.NotFound);
    public static Error Validation(string code, string description) => new(code, description, ErrorType.Validation);
    public static Error Conflict(string code, string description) => new(code, description, ErrorType.Conflict);
    public static Error Failure(string code, string description) => new(code, description, ErrorType.Failure);
    public static Error Unauthorized(string code, string description) => new(code, description, ErrorType.Unauthorized);
    public static Error BusinessRuleViolation(string code, string description) => new(code, description, ErrorType.BusinessRuleViolation);
    public static Error InvalidSortProperty(string sortProperty, string validProperties) => Validation("Application.InvalidSortProperty", $"Invalid sort property: {sortProperty}. Valid properties are: {validProperties}");

}

public enum ErrorType
{
    Failure,
    Unauthorized,
    NotFound,
    Validation,
    BusinessRuleViolation,
    Conflict
}
