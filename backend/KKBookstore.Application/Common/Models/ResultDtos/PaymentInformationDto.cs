namespace KKBookstore.Application.Common.Models.ResultDtos;

public class PaymentInformationDto
{
    public string OrderType { get; set; } = "other";
    public decimal Amount { get; set; }
    public int OrderId { get; set; }
    public string OrderNumber { get; set; }
    public string OrderDescription { get; set; } = "Thanh toán cho đơn hàng";
    public string Name { get; set; }
    public string ReturnUrl { get; set; }
}
