using Microsoft.AspNetCore.Mvc;

using PaymentGateway.Api.Mapings;
using PaymentGateway.Api.Models.Responses;
using PaymentGateway.Core.Models;
using PaymentGateway.Core.Services;

namespace PaymentGateway.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController(PaymentsRepository paymentsRepository) : Controller
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType<GetPaymentResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPaymentResponse>> GetPaymentAsync(Guid id)
    {
        Payment? payment = paymentsRepository.Get(id);

        if (payment == null)
        {
            return new NotFoundObjectResult(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Payment not found"
            });
        }
        else
        {
            var mapper = new GetPaymentResponseMapper();
            var response = mapper.ToDto(payment);

            return new OkObjectResult(response);
        }
    }
}