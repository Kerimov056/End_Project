using EndProject.Application.Abstraction.Services;

namespace EndProject.API.BackGroundServıces;

public class ReservationPickupCheckService : IHostedService
{
    private IServiceProvider _serviceProvider;
    private Timer _timer;

    public ReservationPickupCheckService(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(ReservationReturnCheckService)}Service started....");
        _timer = new Timer(carStautus, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        //_timer = new Timer(otherCarStautus, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    private async void carStautus(object state)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var carServices = scope.ServiceProvider.GetRequiredService<ICarServices>();
            var reservServices = scope.ServiceProvider.GetRequiredService<ICarReservationServices>();
            var chauffeurs = scope.ServiceProvider.GetRequiredService<IChauffeursServices>();


            var today = DateTime.Now;
            Console.WriteLine(today);
            var confirmedReservs = await reservServices.IsResevConfirmedGetAll();



            foreach (var reserv in confirmedReservs)
            {
                Console.WriteLine("YEaa -");
                if (reserv.PickupDate.Minute == today.Minute
                    && reserv.PickupDate.Hour == today.Hour
                    && reserv.PickupDate.Day == today.Day
                    && reserv.PickupDate.Month == today.Month)
                {
                    Console.WriteLine("mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm");
                    await reservServices.StatusNow(reserv.Id);
                    await carServices.ReservCarTrue(reserv.CarId);
                    if (reserv.ChauffeursId is not null)
                    {
                        await chauffeurs.IsChauffeursTrue(reserv.ChauffeursId);
                    }
                }
            }
            Console.WriteLine($"Car Pickup DateTime is {DateTime.Now.ToLongTimeString()}");
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


