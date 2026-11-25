using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Payments.Abstractions;

public interface IPaymentRepository
{
    Task AddAsync(Payment payment);
    Task<Payment?> GetAsync(Guid id);
}
