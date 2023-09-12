using EndProject.Application.Abstraction.Services.Payment.Stripe;
using EndProject.Application.DTOs.Payment;

namespace EndProject.Infrastructure.Services.Payment;

public class StripePayment : IStripePayment
{
    public Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
