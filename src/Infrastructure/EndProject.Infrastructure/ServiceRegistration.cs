using EndProject.Application.Abstraction.Services;
using EndProject.Application.Abstraction.Services.Payment;
using EndProject.Application.Abstraction.Services.Payment.PayPal;
using EndProject.Application.Abstraction.Services.Payment.Stripe;
using EndProject.Application.Abstraction.Services.Stroge;
using EndProject.Infrastructure.Services;
using EndProject.Infrastructure.Services.Azure;
using EndProject.Infrastructure.Services.Email;
using EndProject.Infrastructure.Services.Payment.PayPal;
using EndProject.Infrastructure.Services.Payment.Stripe;
using EndProject.Infrastructure.Services.Stroge;
using EndProject.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace EndProject.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        //File
        //services.AddScoped<IStorgeService, StorgeService();
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<IStorageFile, StorageFile>();
        services.AddScoped<AzureBlobService>();
        //services.AddScoped<ILocalStorage, LocalStorage>();

        //Payment
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IStripePayment, StripePayment>();
        services.AddScoped<IPayPalPayment, PayPalPayment>();
        services.AddScoped<TokenService>();
        services.AddScoped<CustomerService>();
        services.AddScoped<ChargeService>();

        //QRCode
        services.AddScoped<IQRCoderServıces, QRCoderServıces>();
    }
    //LocalStorage
    public static void AddStorageFile<T>(this IServiceCollection services) where T : Storage, IStorageFile
    {
        services.AddScoped<IStorageFile, T>(); 
    }
    public static void AddPayment<T>(this IServiceCollection services) where T : class, IPayment
    {
        services.AddScoped<IPayment, T>();
    }
}
