using EndProject.Domain.Entitys.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndProject.API.BackGroundServıces;

public class BirthDateBGServıces : IHostedService
{
    private IServiceProvider _serviceProvider;
    private Timer _timer;

    public BirthDateBGServıces(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public  Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(BirthDateBGServıces)}Service started....");
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

            var today = DateTime.Today;
            var usersWithBirthday = await scopedProcessingService.Users
                .Where(u => u.BirthDate.HasValue && u.BirthDate.Value.Day == today.Day && u.BirthDate.Value.Month == today.Month)
                .ToListAsync();
            foreach (var user in usersWithBirthday)
            {
                SendBirthdayEmail(user);
            }

            Console.WriteLine($"DateTime is {DateTime.Now.ToLongTimeString()}");
        }
    }

    private void SendBirthdayEmail(AppUser user)
    {
        Console.WriteLine($"Sending birthday email to {user.Email}");
    }

    public  Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        Console.WriteLine($"{nameof(BirthDateBGServıces)}Service stopped....");
        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        _timer = null;
    }
}
