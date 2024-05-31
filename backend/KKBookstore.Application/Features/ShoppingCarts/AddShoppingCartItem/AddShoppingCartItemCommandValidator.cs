using FluentValidation;

namespace KKBookstore.Application.Features.ShoppingCarts.AddShoppingCartItem;

public sealed class AddShoppingCartItemCommandValidator : AbstractValidator<AddShoppingCartItemCommand>
{
    public AddShoppingCartItemCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.SkuId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
