﻿using EndProject.Application.Abstraction.Services;
using EndProject.Domain.Enums.ReservationStatus;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProject.API.BackGroundServıces;

public class ReservationReturnCheckService : IHostedService
{
    private IServiceProvider _serviceProvider;
    private Timer _timer;

    public ReservationReturnCheckService(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(ReservationReturnCheckService)}Service started....");
        _timer = new Timer(carStautus, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        //_timer = new Timer(otherCarStautus, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    private async void carStautus(object state)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var carServices = scope.ServiceProvider.GetRequiredService<ICarServices>();
            var reservServices = scope.ServiceProvider.GetRequiredService<ICarReservationServices>();
            var chauffeurs = scope.ServiceProvider.GetRequiredService<IChauffeursServices>();


            var today = DateTime.Now;
            var confirmedReservs =  await dbContext.CarReservations
                                   .Where(x => x.IsDeleted == false)
                                   .Where(x => x.Status == ReservationStatus.RightNow)
                                   .Where(x => x.ReturnDate.Hour == today.Hour)
                                   .Where(x => x.ReturnDate.Day == today.Day)
                                   .ToListAsync();

            Console.WriteLine("Heleki yo");
            foreach (var reserv in confirmedReservs)
            {
                Console.WriteLine("-----MMMMMMMMMMMMMMMMMMMMMMM-----");
                await carServices.ReservCarFalse(reserv.CarId);
                await reservServices.StatusCompleted(reserv.Id);
                if (reserv.ChauffeursId is not null)
                {
                    await chauffeurs.IsChauffeursFalse(reserv.ChauffeursId);
                }
            }

            Console.WriteLine($"Car Status DateTime is {DateTime.Now.ToLongTimeString()}");
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

