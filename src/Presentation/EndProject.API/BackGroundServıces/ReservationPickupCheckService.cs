﻿using EndProject.Application.Abstraction.Services;

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
        _timer = new Timer(carStautus, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
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


            var today = DateTime.Today;
            var confirmedReservs = await reservServices.IsResevConfirmedGetAll();

            foreach (var reserv in confirmedReservs)
            {
                Console.WriteLine("YEaa -");
                if (reserv.ReturnDate.Hour == today.Hour  && reserv.ReturnDate.Day == today.Day) 
                {
                    await carServices.ReservCarTrue(reserv.CarId);
                    await reservServices.StatusNow(reserv.CarId);
                    if (reserv.ChauffeursId is not null)
                    {
                        await chauffeurs.IsChauffeursTrue(reserv.ChauffeursId);
                    }
                }
            }

            Console.WriteLine($"Car Pickup DateTime is {DateTime.Now.ToLongTimeString()}");
        }
    }


    //private async void otherCarStautus(object state)
    //{
    //    using (IServiceScope scope = _serviceProvider.CreateScope())
    //    {
    //        var carServices = scope.ServiceProvider.GetRequiredService<ICarServices>();
    //        var otherReservServices = scope.ServiceProvider.GetRequiredService<IOtherCarReservationServices>();

    //        var today = DateTime.Today;
    //        var otherConfirimReservs = await otherReservServices.IsResevConfirmedGetAll();

    //        foreach (var reserv in otherConfirimReservs)
    //        {
    //            Console.WriteLine("Other YEaa");
    //            if (reserv.ReturnDate.Day == today.Day && reserv.ReturnDate.Hour == today.Hour && reserv.ReturnDate.Month == today.Month)
    //            {
    //                Console.WriteLine("yes yes");
    //                await carServices.ReservCarTrue(reserv.CarId);
    //            }
    //        }

    //        Console.WriteLine($"Car Status DateTime is {DateTime.Now.ToLongTimeString()}");
    //    }
    //}


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


