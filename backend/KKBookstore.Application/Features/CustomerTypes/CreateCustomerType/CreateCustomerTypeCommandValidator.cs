using FluentValidation;

namespace KKBookstore.Features.CustomerTypes.CreateCustomerType;

public sealed class CreateCustomerTypeCommandValidator : AbstractValidator<CreateCustomerTypeCommand>
{
    public CreateCustomerTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(256)
            .WithMessage("Name is required and cannot exceed 256 characters");

        RuleFor(x => x.Tier)
            .IsInEnum()
            .WithMessage("Invalid customer tier");

        RuleFor(x => x.MinSpending)
            .GreaterThanOrEqualTo(0)
            .WithMessage("MinSpending must be greater than or equal to 0");
    }
}
