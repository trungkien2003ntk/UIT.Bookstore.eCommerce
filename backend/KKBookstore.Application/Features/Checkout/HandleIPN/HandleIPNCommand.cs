using KKBookstore.Common.Interfaces;
using KKBookstore.Constants;
using KKBookstore.Emailing;
using KKBookstore.Emailing.TemplateModels;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace KKBookstore.Features.Checkout.HandleIPN;

public record HandleIPNCommand : IRequest<Result<HandleIPNResponse>>
{
    public string TmnCode { get; set; } = string.Empty;
    public int Amount { get; set; }
    public string BankCode { get; set; } = string.Empty;
    public string BankTranNo { get; set; } = string.Empty;
    public string CardType { get; set; } = string.Empty;
    public string PayDate { get; set; } = string.Empty;
    public string OrderInfo { get; set; } = string.Empty;
    public int TransactionNo { get; set; }
    public string ResponseCode { get; set; } = string.Empty;
    public string TransactionStatus { get; set; } = string.Empty;
    public string TxnRef { get; set; } = string.Empty;
    public string SecureHashType { get; set; } = string.Empty;
    public string SecureHash { get; set; } = string.Empty;
}

public class HandleIPNHandler(
    IApplicationDbContext dbContext,
    ICustomerService customerService,
    IBranchSelectionService branchSelectionService,
    IEmailService emailService,
    IIdentityService identityService,
    ILogger<HandleIPNHandler> logger
) : IRequestHandler<HandleIPNCommand, Result<HandleIPNResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly ICustomerService _customerService = customerService;
    private readonly IBranchSelectionService _branchSelectionService = branchSelectionService;
    private readonly IEmailService _emailService = emailService;
    private readonly IIdentityService _identityService = identityService;
    private readonly ILogger<HandleIPNHandler> _logger = logger;

    public async Task<Result<HandleIPNResponse>> Handle(HandleIPNCommand request, CancellationToken cancellationToken)
    {
        using var dbTransaction = await _dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            // todo: Handle the signature


            // based on the ipn result, persist the transaction to the database
            var responseCode = request.ResponseCode;
            var isSuccess = responseCode == "00";

            var payDate = ConvertNumericDateToDateTimeOffset(request.PayDate);
            var OrderId = GetIdFromTxnRef(request.TxnRef);


            var existingOrder = await _dbContext.Orders
                .Include(o => o.Transactions)
                .Include(o => o.OrderFulfillments)
                    .ThenInclude(of => of.Branch)
                .FirstOrDefaultAsync(o => o.Id == OrderId, cancellationToken);

            if (existingOrder == null)
            {
                await dbTransaction.RollbackAsync(cancellationToken);
                return Result.Failure<HandleIPNResponse>(OrderErrors.OrderNotFound);
            }

            var transaction = new Transaction()
            {
                Amount = request.Amount,
                BankCode = request.BankCode,
                BankTranNo = request.BankTranNo,
                CardType = request.CardType,
                OrderInfo = request.OrderInfo,
                PayDate = payDate,
                ResponseCode = request.ResponseCode,
                TransactionNo = request.TransactionNo,
                TransactionStatus = request.TransactionStatus,
                OrderId = OrderId,
                Order = existingOrder
            };
            existingOrder.PaidWhen = payDate;

            await _dbContext.Transactions.AddAsync(transaction, cancellationToken);

            // Handle order status based on payment result
            if (isSuccess)
            {
                // Payment successful - now handle branch selection logic
                var orderFulfillments = existingOrder.OrderFulfillments.ToList();
                var previousStatus = existingOrder.Status;

                if (_branchSelectionService.RequiresAdminConfirmation(orderFulfillments))
                {
                    // Multiple branches - requires admin confirmation
                    existingOrder.Status = OrderStatus.WaitForConfirmPackageBranch;

                    // Record order history for status change
                    var branchSelectionHistory = OrderHistory.Create(
                        orderId: existingOrder.Id,
                        fromStatus: previousStatus,
                        toStatus: OrderStatus.WaitForConfirmPackageBranch,
                        action: "Thanh toán thành công - Có nhiều chi nhánh khả dụng, đang chờ quản trị viên chọn chi nhánh đóng gói",
                        notes: $"Transaction No: {request.TransactionNo}, Bank: {request.BankCode}",
                        triggeredByUserId: null, // System triggered by payment webhook
                        externalReference: request.TxnRef
                    );

                    if (branchSelectionHistory.IsSuccess)
                    {
                        await _dbContext.OrderHistories.AddAsync(branchSelectionHistory.Value, cancellationToken);
                    }

                    // Notify admins about branch selection needed
                    await NotifyAdminForBranchSelection(existingOrder, orderFulfillments, cancellationToken);
                }
                else if (orderFulfillments.Count == 1)
                {
                    // Single branch - can proceed directly to packaging
                    existingOrder.Status = OrderStatus.Packaging;
                    var singleFulfillment = orderFulfillments.First();
                    singleFulfillment.SelectForPackaging();

                    // Record order history for status change
                    var packagingHistory = OrderHistory.Create(
                        orderId: existingOrder.Id,
                        fromStatus: previousStatus,
                        toStatus: OrderStatus.Packaging,
                        action: "Thanh toán thành công - Chỉ có một chi nhánh khả dụng, tự động chọn để đóng gói",
                        notes: $"Transaction No: {request.TransactionNo}, Bank: {request.BankCode}, Branch: {singleFulfillment.Branch?.Name}",
                        triggeredByUserId: null, // System triggered by payment webhook
                        externalReference: request.TxnRef
                    );

                    if (packagingHistory.IsSuccess)
                    {
                        await _dbContext.OrderHistories.AddAsync(packagingHistory.Value, cancellationToken);
                    }
                }
                else
                {
                    // No fulfillments - this shouldn't happen but handle gracefully
                    // Keep in pending status and log the issue for investigation
                    existingOrder.Status = OrderStatus.Pending;

                    // Record order history for error condition
                    var errorHistory = OrderHistory.Create(
                        orderId: existingOrder.Id,
                        fromStatus: previousStatus,
                        toStatus: OrderStatus.Pending,
                        action: "Thanh toán thành công nhưng không tìm thấy đơn vị thực hiện - cần điều tra",
                        notes: $"Transaction No: {request.TransactionNo}, Bank: {request.BankCode}. Error: No order fulfillments found after payment.",
                        triggeredByUserId: null, // System triggered by payment webhook
                        externalReference: request.TxnRef
                    );

                    if (errorHistory.IsSuccess)
                    {
                        await _dbContext.OrderHistories.AddAsync(errorHistory.Value, cancellationToken);
                    }

                    // TODO: Log this issue for investigation as it indicates a problem with inventory allocation
                }

                // Update customer spending for successful online payments
                var orderTotal = existingOrder.CalculateTotal();
                var customerUpdateResult = await _customerService.UpdateCustomerSpentAmountAsync(
                    existingOrder.CustomerId, orderTotal, cancellationToken);

                if (customerUpdateResult.IsFailure)
                {
                    // Log the error but don't fail the transaction - the payment was successful
                    // The customer update can be retried later if needed
                    // todo: Consider adding a retry mechanism or audit log for failed customer updates
                }
            }
            else
            {
                // Payment failed - keep order in pending status and record history
                var previousStatus = existingOrder.Status;
                existingOrder.Status = OrderStatus.Pending;

                // Record order history for failed payment
                var failureHistory = OrderHistory.Create(
                    orderId: existingOrder.Id,
                    fromStatus: previousStatus,
                    toStatus: OrderStatus.Pending,
                    action: "Thanh toán thất bại - Đơn hàng vẫn đang chờ",
                    notes: $"Transaction No: {request.TransactionNo}, Response Code: {request.ResponseCode}, Bank: {request.BankCode}",
                    triggeredByUserId: null, // System triggered by payment webhook
                    externalReference: request.TxnRef
                );

                if (failureHistory.IsSuccess)
                {
                    await _dbContext.OrderHistories.AddAsync(failureHistory.Value, cancellationToken);
                }

                // Handle specific failure types and return appropriate errors after recording history
                switch (int.Parse(responseCode))
                {
                    case (int)TransactionErrorType.AccountNotRegistered: // 400
                    case (int)TransactionErrorType.ExpiredTransaction:
                    case (int)TransactionErrorType.TransactionCancelled:
                        await _dbContext.SaveChangesAsync(cancellationToken);
                        await dbTransaction.CommitAsync(cancellationToken);
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.BadRequest);

                    case (int)TransactionErrorType.IncorrectAuthentication: // 401
                    case (int)TransactionErrorType.IncorrectPassword:
                    case (int)TransactionErrorType.IncorrectPaymentPassword:
                        await _dbContext.SaveChangesAsync(cancellationToken);
                        await dbTransaction.CommitAsync(cancellationToken);
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.InvalidCredentials);

                    case (int)TransactionErrorType.InsufficientBalance: // 402
                    case (int)TransactionErrorType.ExceededDailyTransactionLimit:
                        await _dbContext.SaveChangesAsync(cancellationToken);
                        await dbTransaction.CommitAsync(cancellationToken);
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.PaymentError);

                    case (int)TransactionErrorType.AccountLocked: // 403
                    case (int)TransactionErrorType.SuspectedFraud:
                        await _dbContext.SaveChangesAsync(cancellationToken);
                        await dbTransaction.CommitAsync(cancellationToken);
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.Forbidden);

                    case (int)TransactionErrorType.BankMaintenance: //503
                        await _dbContext.SaveChangesAsync(cancellationToken);
                        await dbTransaction.CommitAsync(cancellationToken);
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.ServiceUnavailable);

                    case (int)TransactionErrorType.OtherErrors:
                        await _dbContext.SaveChangesAsync(cancellationToken);
                        await dbTransaction.CommitAsync(cancellationToken);
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.Unknown);

                    default:
                        // Unknown error code, still record as generic failure
                        break;
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            await dbTransaction.CommitAsync(cancellationToken);


            return Result.Success(new HandleIPNResponse()
            {
                RspCode = "00",
                Message = "Success"
            });
        }
        catch (Exception)
        {
            await dbTransaction.RollbackAsync(cancellationToken);
            return Result.Failure<HandleIPNResponse>(TransactionErrors.FailedToCommitTransaction);
        }
    }

    private int GetIdFromTxnRef(string txnRef)
    {
        var parts = txnRef.Split('_');
        return int.Parse(parts[0]);
    }

    private async Task NotifyAdminForBranchSelection(Order order, List<OrderFulfillment> orderFulfillments, CancellationToken cancellationToken)
    {
        // Get customer information for the email
        var customer = await _dbContext.Customers
            .FirstOrDefaultAsync(c => c.Id == order.CustomerId, cancellationToken);

        var customerName = customer?.FullName ?? customer?.UserName ?? "Unknown Customer";

        // Build branch options from fulfillments
        var branchOptions = orderFulfillments.Select(of => new BranchSelectionOption
        {
            BranchId = of.BranchId,
            BranchName = of.Branch?.Name ?? $"Branch {of.BranchId}",
            DistanceKm = of.DistanceFromCustomer,
            TotalItems = of.GetTotalAllocatedItems(),
            TotalValue = of.GetTotalAllocatedValue()
        }).ToList();

        var emailModel = new AdminBranchSelectionEmailModel(
            orderId: order.Id,
            orderNumber: order.OrderNumber,
            customerName: customerName,
            orderDate: order.OrderWhen.DateTime,
            branchOptions: branchOptions,
            totalOrderValue: order.CalculateTotal()
        );

        // Get admin users to notify
        var adminUsers = await _identityService.GetUsersInRoleAsync(AppRoles.Admin);
        if (adminUsers.IsFailure || adminUsers.Value.Count == 0)
        {
            _logger.LogWarning("No admin users found to notify for branch selection for order {OrderId}", order.Id);
            return;
        }

        var adminEmails = adminUsers.Value
            .Where(u => !string.IsNullOrEmpty(u.Email))
            .Select(u => u.Email!)
            .ToList();

        if (adminEmails.Count == 0)
        {
            _logger.LogWarning("No admin emails configured for branch selection notifications");
            return;
        }

        // Send email to all admins
        foreach (var adminEmail in adminEmails)
        {
            try
            {
                await _emailService.SendAsync(adminEmail, emailModel.Subject, emailModel);
                _logger.LogInformation("Branch selection notification sent to admin {AdminEmail} for order {OrderId}",
                    adminEmail, order.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send branch selection notification to admin {AdminEmail} for order {OrderId}",
                    adminEmail, order.Id);
            }
        }
    }

    public static DateTimeOffset ConvertNumericDateToDateTimeOffset(string numericDate, int gmtOffset = 7)
    {
        const string format = "yyyyMMddHHmmss";

        try
        {
            DateTime dateTime = DateTime.ParseExact(numericDate, format, CultureInfo.InvariantCulture);
            TimeSpan offset = TimeSpan.FromHours(gmtOffset);
            return new DateTimeOffset(dateTime, offset);
        }
        catch (FormatException)
        {
            throw new ArgumentException("Invalid numeric date format. Expected yyyyMMddHHmmss.");
        }
    }
}
