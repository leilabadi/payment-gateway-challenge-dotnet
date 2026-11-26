using System.Net;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using PaymentGateway.Api.Controllers;
using PaymentGateway.Api.Features.Payments.GetPayment;
using PaymentGateway.Application;
using PaymentGateway.Application.Payments.Abstractions;
using PaymentGateway.Domain.Payments.Entities;
using PaymentGateway.Infrastructure;

namespace PaymentGateway.Api.Tests;

public class PaymentsControllerTests
{
    private readonly Random _random = new();

    [Fact]
    public async Task RetrievesAPaymentSuccessfully()
    {
        // Arrange
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            ExpiryYear = _random.Next(2023, 2030),
            ExpiryMonth = _random.Next(1, 12),
            Amount = _random.Next(1, 10000),
            CardNumberLastFour = _random.Next(1111, 9999),
            Currency = "GBP"
        };

        var webApplicationFactory = new WebApplicationFactory<PaymentsController>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(services =>
            {
                ((ServiceCollection)services).AddApplication()
                    .AddInfrastructure();
            }));

        using var scope = webApplicationFactory.Services.CreateScope();
        var paymentRepository = scope.ServiceProvider.GetRequiredService<IPaymentRepository>();

        await paymentRepository.AddAsync(payment);

        HttpClient client = webApplicationFactory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/Payments/{payment.Id}");
        var paymentResponse = await response.Content.ReadFromJsonAsync<GetPaymentResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(paymentResponse);
    }

    [Fact]
    public async Task Returns404IfPaymentNotFound()
    {
        // Arrange
        var webApplicationFactory = new WebApplicationFactory<PaymentsController>();
        var client = webApplicationFactory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/Payments/{Guid.NewGuid()}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}