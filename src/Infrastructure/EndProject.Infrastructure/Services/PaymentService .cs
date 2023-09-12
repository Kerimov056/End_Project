using EndProject.Application.Abstraction.Services.Payment;
using EndProject.Application.DTOs.Payment;

namespace EndProject.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly IPayment _payment;

    public PaymentService(IPayment payment) => _payment = payment;

    public Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken)
     =>  _payment.CreateCharge(resource, cancellationToken);

    public Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken)
     =>  _payment.CreateCustomer(resource, cancellationToken);

}