using EndProject.Application.Abstraction.Services;
using EndProject.Domain.Entitys.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndProject.API.BackGroundServıces;

public class BirthDateBGServices : IHostedService
{
    private IServiceProvider _serviceProvider;
    private Timer _timer;

    public BirthDateBGServices(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(BirthDateBGServices)}Service started....");
        _timer = new Timer(writeDateTimeOnLog, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        return Task.CompletedTask;
    }

    private async void writeDateTimeOnLog(object state)
    {
        //Console.WriteLine("Salam");
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            UserManager<AppUser> scopedProcessingService =
                scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            var today = DateTime.Today;
            var usersWithBirthday = await scopedProcessingService.Users
                .Where(u => u.BirthDate.HasValue && u.BirthDate.Value.Day == today.Day && u.BirthDate.Value.Month == today.Month)
                .ToListAsync();

            foreach (var user in usersWithBirthday)
            {
                string subject = "Happy Birthday.";
                string html = string.Empty;

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "HappyBirthday.html");
                html = System.IO.File.ReadAllText(filePath);

                emailService.Send(user.Email, subject, html);
            }

            Console.WriteLine($"DateTime is {DateTime.Now.ToLongTimeString()}");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        Console.WriteLine($"{nameof(BirthDateBGServices)}Service stopped....");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer = null;
    }
}
