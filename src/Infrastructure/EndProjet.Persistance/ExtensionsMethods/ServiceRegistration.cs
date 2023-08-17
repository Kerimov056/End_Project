using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.Validators.SliderValidators;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Context;
using EndProjet.Persistance.Implementations.Repositories.EntityRepository;
using EndProjet.Persistance.Implementations.Services;
using EndProjet.Persistance.MapperProfiles;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
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
        services.AddScoped<ISliderReadRepository, SliderReadRepository>();
        services.AddScoped<ISliderWriteRepository, SliderWriteRepository>();
        services.AddScoped<ICarTypeWriteRepository, CarTypeWriteRepository>();
        services.AddScoped<ICarTypeReadRepository, CarTypeReadRepository>();
        services.AddScoped<ICarReadRepository, CarReadRepository>();
        services.AddScoped<ICarWriteRepository, CarWriteRepository>();
        services.AddScoped<ICarImageReadRepository, CarImageReadRepository>();
        services.AddScoped<ICarImageWriteRepository, CarImageWriteRepository>();
        services.AddScoped<ICarCategoryReadRepository, CarCategoryReadRepository>();
        services.AddScoped<ICarCategoryWriteRepository, CarCategoryWriteRepository>();


        //Service
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISliderService, SliderService>();
        services.AddScoped<ICarTypeService, CarTypeService>();
        services.AddScoped<ICarServices, CarServices>();
        services.AddScoped<ICarImageServices, CarImageServices>();
      



        //User
        services.AddIdentity<AppUser, IdentityRole>(Options =>
        {
            Options.User.RequireUniqueEmail = true;
            Options.Password.RequireNonAlphanumeric = true;
            Options.Password.RequiredLength = 8;
            Options.Password.RequireDigit = true;
            Options.Password.RequireUppercase = true;
            Options.Password.RequireLowercase = true;

            Options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            Options.Lockout.MaxFailedAccessAttempts = 3;
            Options.Lockout.AllowedForNewUsers = true;
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

        
        //Mapper
        services.AddAutoMapper(typeof(SliderProfile).Assembly);

        //Validator
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<SliderGetDtoValidator>();
    }
}
