using EndProject.Application.Abstraction.Services;
using EndProject.Domain.Enums.CampaignsStatus;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProject.API.BackGroundServıces;

public class PicakUpCompaignsBackService : IHostedService
{

    private IServiceProvider _serviceProvider;
    private Timer _timer;

    public PicakUpCompaignsBackService(IServiceProvider serviceProvider)
     => _serviceProvider = serviceProvider;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(PicakUpCompaignsBackService)}Service started....");
        _timer = new Timer(carCompagin, null, TimeSpan.Zero, TimeSpan.FromSeconds(40));
        return Task.CompletedTask;
    }

    private async void carCompagin(object state)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var carServices = scope.ServiceProvider.GetRequiredService<ICarServices>();

            bool isComp = false;

            var today = DateTime.Now;
            var comaignStart = await dbContext.Cars
                               .Where(x => x.Status == CampaignsStatus.CampaignTrue)
                               .Where(x => x.PickUpCampaigns.Value.Day == today.Day)
                               .Where(x => x.PickUpCampaigns.Value.Hour == today.Hour)
                               .Where(x => x.PickUpCampaigns.Value.Minute == today.Minute)
                               .FirstOrDefaultAsync();
            
            Console.WriteLine("Campagns");
            if (comaignStart is not null && isComp == false)
            {
                isComp = true;
                Console.WriteLine("Campagns one start");
                await carServices.CompaignsChangePrice();
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        Console.WriteLine($"{nameof(PicakUpCompaignsBackService)}Service stopped....");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer = null;
    }
}
