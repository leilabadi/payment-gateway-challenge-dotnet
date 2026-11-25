using MediatR;

using Microsoft.AspNetCore.Mvc;

using PaymentGateway.Api.Features.Payments.GetPayment;
using PaymentGateway.Api.Mapings;
using PaymentGateway.Application.Payments.GetPayment;

namespace PaymentGateway.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController(ISender sender) : Controller
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType<GetPaymentResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPaymentResponse>> GetPaymentAsync(Guid id)
    {
        GetPaymentResult? response = await sender.Send(new GetPaymentQuery(id));

        if (response == null)
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
            return new OkObjectResult(mapper.ToContract(response));
        }
    }
}