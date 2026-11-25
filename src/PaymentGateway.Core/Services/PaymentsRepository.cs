using PaymentGateway.Core.Models;

namespace PaymentGateway.Core.Services;

public class PaymentsRepository
{
    public List<Payment> Payments = [];
    
    public void Add(Payment payment)
    {
        Payments.Add(payment);
    }

    public Payment? Get(Guid id)
    {
        return Payments.FirstOrDefault(p => p.Id == id);
    }
}