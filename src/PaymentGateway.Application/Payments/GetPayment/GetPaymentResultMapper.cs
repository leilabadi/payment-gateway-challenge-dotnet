using PaymentGateway.Domain.Payments.Entities;

using Riok.Mapperly.Abstractions;

namespace PaymentGateway.Application.Payments.GetPayment;

[Mapper]
public partial class GetPaymentResultMapper
{
    public partial GetPaymentResult ToDto(Payment response);
}
