using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Common.Interfaces;

public interface IPaymentService
{
    string CreatePaymentUrl(PaymentInformationDto request, string ipAddress);

}

