using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace KKBookstore.Application.Features.Checkout.HandleIPN;

public record HandleIPNCommand : IRequest<Result<HandleIPNResponse>>
{
    public string TmnCode { get; set; }
    public int Amount { get; set; }
    public string BankCode { get; set; }
    public string BankTranNo { get; set; }
    public string CardType { get; set; }
    public string PayDate { get; set; }
    public string OrderInfo { get; set; }
    public int TransactionNo { get; set; }
    public string ResponseCode { get; set; }
    public string TransactionStatus { get; set; }
    public string TxnRef { get; set; }
    public string SecureHashType { get; set; }
    public string SecureHash { get; set; }
}

public class HandleIPNHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<HandleIPNCommand, Result<HandleIPNResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

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

            await _dbContext.SaveChangesAsync(cancellationToken);
            await dbTransaction.CommitAsync(cancellationToken);


            return Result.Success(new HandleIPNResponse()
            {
                RspCode = "00",
                Message = "Success"
            });
        }
        catch (Exception ex)
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
