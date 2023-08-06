using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.Validators.PostValidator;
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
        services.AddScoped<IPostReadRepository, PostReadRepository>();
        services.AddScoped<IPostWriteRepository, PostWriteRepository>(); 
        services.AddScoped<IPostImageReadRepository, PostImageReadRepository>();
        services.AddScoped<IPostImageWriteRepository, PostImageWriteRepository>();
        services.AddScoped<ITagReadRepository, TagReadRepository>();
        services.AddScoped<ITagWriteRepository, TagWriteRepository>();
        services.AddScoped<INewTagReadRepository, NewTagReadRepository>();
        services.AddScoped<INewTagWriteRepository, NewTagWriteRepository>();
        services.AddScoped<ICommentReadRepository, CommentReadRepository>();
        services.AddScoped<ICommentWriteRepository, CommentWriteRepository>();



        //Service
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPostImageService, PostImageService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<INewTagService, NewTagService>();
        services.AddScoped<ICommentService, CommentService>();



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
        services.AddAutoMapper(typeof(PostProfile).Assembly);

        //Validator
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<PostImageGetDtoValidator>();
    }
}
