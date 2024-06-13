using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using Microsoft.Extensions.Options;

namespace KKBookstore.Infrastructure.Payment;

public class VnPayPaymentService(IOptions<VnPayConfiguration> options) : IPaymentService
{
    private VnPayConfiguration _configuration = options.Value;

    public string CreatePaymentUrl(PaymentInformationDto request, string ipAddress)
    {
        var timeNow = DateTime.Now;
        var tick = DateTime.Now.Ticks.ToString();
        var pay = new VnPayHelper();
        var urlCallBack = request.ReturnUrl;

        pay.AddRequestData("vnp_Version", _configuration.Version);
        pay.AddRequestData("vnp_Command", _configuration.Command);
        pay.AddRequestData("vnp_TmnCode", _configuration.TmnCode);
        pay.AddRequestData("vnp_Amount", ((int)request.Amount * 100).ToString());
        pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
        pay.AddRequestData("vnp_CurrCode", _configuration.CurrCode);
        pay.AddRequestData("vnp_IpAddr", ipAddress);
        pay.AddRequestData("vnp_Locale", _configuration.Locale);
        pay.AddRequestData("vnp_OrderInfo", $"{request.OrderDescription} {request.OrderNumber}: {request.Amount} {_configuration.CurrCode}");
        pay.AddRequestData("vnp_OrderType", request.OrderType);
        pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
        pay.AddRequestData("vnp_TxnRef", $"{request.OrderId.ToString()}_{tick}");

        var paymentUrl =
            pay.CreateRequestUrl(_configuration.BaseUrl, _configuration.HashSecret);

        return paymentUrl;
    }
}
