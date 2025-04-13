using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Common.Interfaces;

public interface IPaymentService
{
    string CreatePaymentUrl(PaymentInformationDto request, string ipAddress);

}

