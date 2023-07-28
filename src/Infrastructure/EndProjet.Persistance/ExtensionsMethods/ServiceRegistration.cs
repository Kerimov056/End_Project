using EndProject.Application.Abstraction.Repositories;
using EndProjet.Persistance.Context;
using EndProjet.Persistance.Implementations.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EndProjet.Persistance.ExtensionsMethods;

public static class ServiceRegistration
{
    public static void AddPersistenceServices (this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(services.BuildServiceProvider().GetService<IConfiguration>().GetConnectionString("Default"));
        });

        //Repository
    }
}
