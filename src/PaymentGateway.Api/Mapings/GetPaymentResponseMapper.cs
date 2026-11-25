using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Core.Models;

using Riok.Mapperly.Abstractions;

namespace PaymentGateway.Api.Mapings;

[Mapper]
public partial class GetPaymentResponseMapper
{
    public partial GetPaymentResponse ToDto(Payment response);
}
