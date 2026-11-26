using MediatR;

using PaymentGateway.Application.Payments.Abstractions;
using PaymentGateway.Domain.Payments.Enums;

namespace PaymentGateway.Application.Payments.GetPayment;

public record GetPaymentQuery(Guid Id) : IRequest<GetPaymentResult?>;

public record GetPaymentResult
(
    Guid Id,
    PaymentStatus Status,
    int CardNumberLastFour,
    int ExpiryMonth,
    int ExpiryYear,
    string Currency,
    int Amount
);

public class GetPaymentQueryHandler(IPaymentRepository repository)
    : IRequestHandler<GetPaymentQuery, GetPaymentResult?>
{
    public async Task<GetPaymentResult?> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        var payment = await repository.GetAsync(request.Id);

        return payment != null
            ? new GetPaymentResultMapper().ToDto(payment)
            : null;
    }
}
