using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;

namespace KKBookstore.Application.Features.Checkout.PlaceOrder;

public abstract class OrderProcessor(
    IApplicationDbContext dbContext,
    IPaymentService paymentService,
    IEmailService emailService
)
{
    protected readonly IApplicationDbContext _dbContext = dbContext;
    protected readonly IPaymentService _paymentService = paymentService;
    protected readonly IEmailService _emailService = emailService;

    public async Task<Result<PlaceOrderResponse>> ProcessOrder(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            var checkoutItems = await GetCheckoutItems(request, cancellationToken);

            if (!await CheckInventory(checkoutItems, cancellationToken))
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure<PlaceOrderResponse>(OrderErrors.InsufficientStock);
            }

            await ReduceStock(checkoutItems, cancellationToken);
            await RemoveFromCart(checkoutItems, cancellationToken);

            var order = await CreateOrder(request, checkoutItems, cancellationToken);
            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (!await ApplyDiscountVouchers(order, request, cancellationToken))
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure<PlaceOrderResponse>(OrderErrors.DiscountVoucherNotAvailable);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            var paymentUrlResult = await HandlePayment(request, order, cancellationToken);
            if (paymentUrlResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure<PlaceOrderResponse>(paymentUrlResult.Error);
            }
            var paymentUrl = paymentUrlResult.Value;

            var result = new PlaceOrderResponse()
            {
                OrderId = order.Id,
                PaymentUrl = paymentUrl
            };

            await SendOrderConfirmation(request.UserId, order, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            // todo: log exception
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<PlaceOrderResponse>(Error.Validation("erorr", ex.Message));
        }
    }

    protected abstract Task<List<ShoppingCartItem>> GetCheckoutItems(PlaceOrderCommand request, CancellationToken cancellationToken);
    protected abstract Task<bool> CheckInventory(List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken);
    protected abstract Task ReduceStock(List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken);
    protected abstract Task RemoveFromCart(List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken);
    protected abstract Task<Order> CreateOrder(PlaceOrderCommand request, List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken);
    protected abstract Task<bool> ApplyDiscountVouchers(Order order, PlaceOrderCommand request, CancellationToken cancellationToken);
    protected abstract Task<Result<string>> HandlePayment(PlaceOrderCommand request, Order order, CancellationToken cancellationToken);
    protected abstract Task SendOrderConfirmation(int userId, Order order, CancellationToken cancellationToken);
}
