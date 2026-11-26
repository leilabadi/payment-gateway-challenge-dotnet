using PaymentGateway.Application.Payments.Abstractions;
using PaymentGateway.Application.Payments.GetPayment;
using PaymentGateway.Domain.Payments.Entities;

namespace PaymentGateway.Application.Tests.Payments.GetPayment;

public class GetPaymentQueryHandlerTests
{
    private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
    private readonly GetPaymentQueryHandler _sut;

    public GetPaymentQueryHandlerTests()
    {
        _paymentRepositoryMock = new Mock<IPaymentRepository>();
        _sut = new GetPaymentQueryHandler(_paymentRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenPaymentExists_ReturnsIt()
    {
        // Arrange
        var query = new GetPaymentQuery(Guid.NewGuid());

        // Returning a matching payment
        _paymentRepositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(CreatePayment(query.Id));

        // Act
        var result = await _sut.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(query.Id);
        _paymentRepositoryMock.Verify(x => x.GetAsync(It.Is<Guid>(x => x == query.Id)), Times.Once());
    }

    [Fact]
    public async Task Handle_WhenPaymentDoesNotExists_ReturnsNull()
    {
        // Arrange
        var query = new GetPaymentQuery(Guid.NewGuid());

        // Returning null to simulate non-existing payment
        _paymentRepositoryMock
            .Setup(x => x.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Payment?)null);

        // Act
        var result = await _sut.Handle(query, CancellationToken.None);

        // Assert
        result.ShouldBeNull();
        _paymentRepositoryMock.Verify(x => x.GetAsync(It.Is<Guid>(x => x == query.Id)), Times.Once());
    }

    private static Payment CreatePayment(Guid id)
    {
        return new Payment
        {
            Id = id,
            Currency = "GBP"
        };
    }
}
