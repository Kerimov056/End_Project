using EndProject.Application.Abstraction.Services;
using EndProject.Infrastructure.Services;
using EndProject.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace EndProject.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<IStorageFile, StorageFile>();
        services.AddScoped<IStorgeService, StorgeService>();
    }
                                                                                      //LocalStorage
    public static void AddStorageFile<T>(this IServiceCollection services) where T : Services.Storage, IStorageFile
    {
        services.AddScoped<IStorageFile, T>(); 
    }
}
