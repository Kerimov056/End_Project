using EndProject.Application.Abstraction.Services;
using EndProject.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace EndProject.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, TokenHandler>();
    }
}
