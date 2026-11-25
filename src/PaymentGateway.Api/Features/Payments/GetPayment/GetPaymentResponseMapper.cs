using PaymentGateway.Api.Features.Payments.GetPayment;
using PaymentGateway.Application.Payments.GetPayment;

using Riok.Mapperly.Abstractions;

namespace PaymentGateway.Api.Mapings;

[Mapper]
public partial class GetPaymentResponseMapper
{
    public partial GetPaymentResponse ToContract(GetPaymentResult result);
}
