using EndProject.Application.Abstraction.Services.Payment.PayPal;
using EndProject.Application.DTOs.Payment;

namespace EndProject.Infrastructure.Services.Payment.PayPal;

public class PayPalPayment : IPayPalPayment
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
