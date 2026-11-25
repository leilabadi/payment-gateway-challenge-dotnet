using Microsoft.Extensions.DependencyInjection;

using PaymentGateway.Application.Payments.Abstractions;
using PaymentGateway.Infrastructure.Persistence;

namespace PaymentGateway.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IPaymentRepository, PaymentRepository>();

        return services;
    }
}
