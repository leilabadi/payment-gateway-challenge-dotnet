using PaymentGateway.Application.Payments.Abstractions;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Infrastructure.Persistence;

public class PaymentRepository : IPaymentRepository
{
    public List<Payment> Payments = [];

    public Task AddAsync(Payment payment)
    {
        Payments.Add(payment);
        return Task.CompletedTask;
    }

    public Task<Payment?> GetAsync(Guid id)
    {
        var payment = Payments.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(payment);
    }
}
