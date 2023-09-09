using EndProject.Application.Abstraction.Services;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProject.API.BackGroundServıces;

public class ReturnCompaignsBackService : IHostedService
{

    private IServiceProvider _serviceProvider;
    private Timer _timer;

    public ReturnCompaignsBackService(IServiceProvider serviceProvider)
     =>  _serviceProvider = serviceProvider;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(ReturnCompaignsBackService)}Service started....");
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
            var comaignStart = await dbContext.Cars
                              .Where(x => x.isCampaigns == true)
                              .Where(x => x.ReturnCampaigns.Value.Day == today.Day)
                              .Where(x => x.ReturnCampaigns.Value.Hour == today.Hour)
                              .ToListAsync();

            Console.WriteLine("Campagns");
            foreach (var item in comaignStart)
            {
                Console.WriteLine("Campagns one start");
                await carServices.CompaignsReturn();
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        Console.WriteLine($"{nameof(ReturnCompaignsBackService)}Service stopped....");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer = null;
    }
}