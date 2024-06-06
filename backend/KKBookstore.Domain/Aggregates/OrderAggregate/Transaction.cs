using KKBookstore.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKBookstore.Domain.Aggregates.OrderAggregate;

public class Transaction : BaseEntity
{
    public Transaction()
    {
        
    }

    public int Amount { get; set; }
    public string BankCode { get; set; }
    public string BankTranNo { get; set; }
    public string CardType { get; set; }
    public string OrderInfo { get; set; }
    public DateTimeOffset PayDate { get; set; }
    public string ResponseCode { get; set; }
    public int TransactionNo { get; set; }
    public string TransactionStatus { get; set; }
    public int OrderId { get; set; }
    

    // navigation properties
    public Order Order { get; set; }



    [NotMapped]
    public string TransactionStatusMessage => TransactionStatus switch
    {
        "00" => "Giao dịch thanh toán được thực hiện thành công tại VNPAY",
        _ => "Giao dịch không thành công tại VNPAY"
    };

    [NotMapped]
    public TransactionErrorType ResponseType => (TransactionErrorType)int.Parse(ResponseCode);
}
