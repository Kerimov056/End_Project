﻿using EndProject.Application.Abstraction.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EndProject.API.BackGroundServıces;

public class ReservationCheckService : IHostedService
{
    private IServiceProvider _serviceProvider;
    private Timer _timer;

    public ReservationCheckService(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(ReservationCheckService)}Service started....");
        _timer = new Timer(carStautus, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    private async void carStautus(object state)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var carServices = scope.ServiceProvider.GetRequiredService<ICarServices>();
            var reservServices = scope.ServiceProvider.GetRequiredService<ICarReservationServices>();

            var today = DateTime.Today;
            var confirmedReservs = await reservServices.IsResevConfirmedGetAll();

            foreach (var reserv in confirmedReservs)
            {
                Console.WriteLine("YEaaaaaaaaaaaaaaaYEYEYEYEYY");
                if (reserv.ReturnDate.Day == today.Day && reserv.ReturnDate.Hour == today.Hour)
                {
                    Console.WriteLine("yes yes");
                    carServices.ReservCarFalse(reserv.CarId);
                }
            }

            Console.WriteLine($"Car Status DateTime is {DateTime.Now.ToLongTimeString()}");
        }
    }


    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        Console.WriteLine($"{nameof(ReservationCheckService)}Service stopped....");
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer = null;
    }
}

