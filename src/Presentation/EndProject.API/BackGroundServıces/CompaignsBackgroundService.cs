using EndProject.Application.Abstraction.Services;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProject.API.BackGroundServıces;

public class CompaignsBackgroundService : IHostedService
{

    private IServiceProvider _serviceProvider;
    private Timer _timer;

    public CompaignsBackgroundService(IServiceProvider serviceProvider, Timer timer)
    {
        _serviceProvider = serviceProvider;
        _timer = timer;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(ReservationReturnCheckService)}Service started....");
        _timer = new Timer(carCompagin, null, TimeSpan.Zero, TimeSpan.FromSeconds(59));
        return Task.CompletedTask;
    }

    private async void carCompagin(object state)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var carServices = scope.ServiceProvider.GetRequiredService<ICarServices>();


            var today = DateTime.Now;
            var comaignStart = await dbContext.Cars.Where(x => x.PickUpCampaigns == today).ToListAsync();
            Console.WriteLine("Campagns");
            foreach (var item in comaignStart)
            {
                Console.WriteLine("Campagns one start");
                await carServices.CompaignsChangePrice();
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        Console.WriteLine($"{nameof(ReservationReturnCheckService)}Service stopped....");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer = null;
    }
}
