using EndProject.Application.Abstraction.Services;

namespace EndProject.API.BackGroundServıces;

public class ReservationCheckService : BackgroundService
{
    private readonly ICarReservationServices _carReservationServices;
    //private
    public ReservationCheckService(ICarReservationServices carReservationServices)
        => _carReservationServices = carReservationServices;
    private Timer _timer;
        
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //await


            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }

    private async Task CheckReservationsAsync()
    {
        var ByCars = await _carReservationServices.GetAllAsync();
        var today = DateTime.Today;
        foreach (var car in ByCars)
        {
            if (car.ReturnDate.Day == today.Day && car.ReturnDate.Month == today.Month && car.ReturnDate.Year == today.Year)
            {
                
            }   
        }
    }
}
