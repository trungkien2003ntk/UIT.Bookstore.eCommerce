using KKBookstore.Common.Interfaces;
using KKBookstore.Emailing;
using KKBookstore.Models;
using KKBookstore.Orders;
using KKBookstore.ShoppingCarts;

namespace KKBookstore.Features.Checkout.PlaceOrder;

public abstract class OrderProcessor(
    IApplicationDbContext dbContext,
    IPaymentService paymentService,
    IEmailSender emailService
)
{
    protected readonly IApplicationDbContext _dbContext = dbContext;
    protected readonly IPaymentService _paymentService = paymentService;
    protected readonly IEmailSender _emailService = emailService;    public async Task<Result<PlaceOrderResponse>> ProcessOrder(PlaceOrderCommand request, CancellationToken cancellationToken)
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

            var order = await CreateOrder(request, checkoutItems, cancellationToken);
            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // NEW: Intelligent branch selection and inventory allocation
            var allocationResult = await AllocateInventoryFromNearestBranches(order, request, cancellationToken);
            if (allocationResult.IsFailure)
            {
                await transaction.RollbackAsync(cancellationToken);
                return Result.Failure<PlaceOrderResponse>(allocationResult.Error);
            }

            var orderFulfillments = allocationResult.Value;

            // Determine if admin confirmation is needed for packaging branch selection
            var requiresAdminConfirmation = RequiresAdminConfirmation(orderFulfillments);
            if (requiresAdminConfirmation)
            {
                order.Status = OrderStatus.WaitForConfirmPackageBranch;
                await NotifyAdminForBranchSelection(order, orderFulfillments, cancellationToken);
            }
            else
            {
                // Single branch - can proceed directly to packaging
                order.Status = OrderStatus.Packaging;
                var singleFulfillment = orderFulfillments.First();
                singleFulfillment.SelectForPackaging();
            }

            // Now reduce the actual stock
            await ReduceStock(checkoutItems, orderFulfillments, cancellationToken);
            await RemoveFromCart(checkoutItems, cancellationToken);

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
            return Result.Failure<PlaceOrderResponse>(Error.Validation("error", ex.Message));
        }
    }    protected abstract Task<List<ShoppingCartItem>> GetCheckoutItems(PlaceOrderCommand request, CancellationToken cancellationToken);
    protected abstract Task<bool> CheckInventory(List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken);
    protected abstract Task ReduceStock(List<ShoppingCartItem> checkoutItems, List<OrderFulfillment> orderFulfillments, CancellationToken cancellationToken);
    protected abstract Task RemoveFromCart(List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken);
    protected abstract Task<Order> CreateOrder(PlaceOrderCommand request, List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken);
    protected abstract Task<bool> ApplyDiscountVouchers(Order order, PlaceOrderCommand request, CancellationToken cancellationToken);
    protected abstract Task<Result<string>> HandlePayment(PlaceOrderCommand request, Order order, CancellationToken cancellationToken);
    protected abstract Task SendOrderConfirmation(int userId, Order order, CancellationToken cancellationToken);
    
    // NEW: Intelligent branch selection methods
    protected abstract Task<Result<List<OrderFulfillment>>> AllocateInventoryFromNearestBranches(Order order, PlaceOrderCommand request, CancellationToken cancellationToken);
    protected abstract bool RequiresAdminConfirmation(List<OrderFulfillment> orderFulfillments);
    protected abstract Task NotifyAdminForBranchSelection(Order order, List<OrderFulfillment> orderFulfillments, CancellationToken cancellationToken);
}
