using FluentValidation;

namespace KKBookstore.Features.CustomerTypes.UpdateCustomerType;

public sealed class UpdateCustomerTypeCommandValidator : AbstractValidator<UpdateCustomerTypeCommand>
{
    public UpdateCustomerTypeCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0");

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
