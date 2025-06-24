using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    ICustomerService customerService
) : IRequestHandler<HandleIPNCommand, Result<HandleIPNResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly ICustomerService _customerService = customerService;

    public async Task<Result<HandleIPNResponse>> Handle(HandleIPNCommand request, CancellationToken cancellationToken)
    {
        using var dbTransaction = await _dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            // todo: Handle the signature


            // based on the ipn result, persist the transaction to the database
            var responseCode = request.ResponseCode;
            var isSuccess = responseCode == "00";

            if (!isSuccess)
            {
                switch (int.Parse(responseCode))
                {
                    case (int)TransactionErrorType.AccountNotRegistered: // 400
                    case (int)TransactionErrorType.ExpiredTransaction:
                    case (int)TransactionErrorType.TransactionCancelled:
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.BadRequest);

                    case (int)TransactionErrorType.IncorrectAuthentication: // 401
                    case (int)TransactionErrorType.IncorrectPassword:
                    case (int)TransactionErrorType.IncorrectPaymentPassword:
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.InvalidCredentials);

                    case (int)TransactionErrorType.InsufficientBalance: // 402
                    case (int)TransactionErrorType.ExceededDailyTransactionLimit:
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.PaymentError);

                    case (int)TransactionErrorType.AccountLocked: // 403
                    case (int)TransactionErrorType.SuspectedFraud:
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.Forbidden);

                    case (int)TransactionErrorType.BankMaintenance: //503
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.ServiceUnavailable);

                    case (int)TransactionErrorType.OtherErrors:
                        return Result.Failure<HandleIPNResponse>(TransactionErrors.Unknown);

                    default:
                        break;
                }
            }

            var payDate = ConvertNumericDateToDateTimeOffset(request.PayDate);
            var OrderId = GetIdFromTxnRef(request.TxnRef);


            var existingOrder = await _dbContext.Orders
                .Include(o => o.Transactions)
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
            existingOrder.Status = isSuccess ? OrderStatus.Processing : OrderStatus.Pending;

            await _dbContext.Transactions.AddAsync(transaction, cancellationToken);

            // Update customer spending for successful online payments
            if (isSuccess)
            {
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

            await _dbContext.SaveChangesAsync(cancellationToken);
            await dbTransaction.CommitAsync(cancellationToken);


            return Result.Success(new HandleIPNResponse()
            {
                RspCode = "00",
                Message = "Success"
            });
        }        catch (Exception)
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
