using EndProject.Application.Abstraction.Services.Payment.Stripe;
using EndProject.Application.DTOs.Payment;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StripeController : ControllerBase
{
    private readonly IStripePayment _stripePayment;

    public StripeController(IStripePayment stripePayment)
     => _stripePayment = stripePayment;

    [HttpPost("createCharge")]
    public async Task<IActionResult> CreateCharge([FromBody] CreateChargeResource chargeResource)
    {
        var chargeResult = await _stripePayment.CreateCharge(chargeResource, HttpContext.RequestAborted);
        return Ok(chargeResult);
    }


    [HttpPost("createCustomer")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerResource customerResource)
    {
        var customerResult = await _stripePayment.CreateCustomer(customerResource, HttpContext.RequestAborted);
        return Ok(customerResult);
    }
}
