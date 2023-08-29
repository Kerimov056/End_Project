//using SignalRChat.Hubs;
using EndProject.API.BackGroundServıces;
using EndProject.API.Hub;
using EndProject.Application.Abstraction.Services;
using EndProject.Domain.Helpers.AccountSetting;
using EndProject.Infrastructure;
using EndProject.Infrastructure.Services.Email;
using EndProjet.Persistance.Context;
using EndProjet.Persistance.ExtensionsMethods;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SymmetricSecurityKey = Microsoft.IdentityModel.Tokens.SymmetricSecurityKey;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddCors();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<EmailSetting>();


builder.Services.AddScoped<AppDbContextInitializer>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
        LifetimeValidator = (_, expire, _, _) => expire > DateTime.UtcNow,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<BirthDateBGServices>();
builder.Services.AddHostedService<ReservationPickupCheckService>();
builder.Services.AddHostedService<ReservationReturnCheckService>();

builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, UserConnection>>(opts => new Dictionary<string, UserConnection>());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var instance = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await instance.InitializeAsync();
    await instance.RoleSeedAsync();
    await instance.UserSeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapHub<ChatHub>("/chat");

app.UseCors();
app.UseCors(cors => cors
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(x => true)
            .AllowCredentials());

//app.UseHttpsRedirection();
//app.UseRouting(); // Bu satırı ekleyin


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
