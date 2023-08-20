using EndProject.Application.Abstraction.Services;

namespace EndProject.API.BackGroundServıces;

public class ReservationCheckService : IHostedService
{
    private IServiceProvider _serviceProvider;
    private readonly ICarReservationServices _carReservationServices;
    private readonly ICarServices _carServices;
    private Timer _timer;

    public ReservationCheckService(
        IServiceProvider serviceProvider,
        ICarReservationServices carReservationServices,
        ICarServices carServices)
    {
        _serviceProvider = serviceProvider;
        _carReservationServices = carReservationServices;
        _carServices = carServices;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"{nameof(BirthDateBGServices)}Service started....");
        _timer = new Timer(carStautus, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
        return Task.CompletedTask;
    }

    private async void carStautus(object state)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var today = DateTime.Today;
            var confirmedReservs = await _carReservationServices.IsResevConfirmedGetAll();

            foreach (var item in confirmedReservs)
            {

            }

            Console.WriteLine($"Car Status DateTime is {DateTime.Now.ToLongTimeString()}");
        }
    }

    private async void IsReservFalse(Guid carId)
    {
        await _carServices.i
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}



//private readonly ICarReservationServices _carReservationServices;
//private readonly ICarServices _carServices;
//public ReservationCheckService(ICarReservationServices carReservationServices)
//    => _carReservationServices = carReservationServices;

//protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//{
//    while (!stoppingToken.IsCancellationRequested)
//    {
//        await CheckReservationsAsync();

//        await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
//        Console.WriteLine($"DateTime is {DateTime.Now.ToLongTimeString()}");

//    }
//}

//private async Task CheckReservationsAsync()
//{
//    var today = DateTime.Today;
//    var ReservsCars = await _carReservationServices.IsResevConfirmedGetAll();
//    foreach (var car in ReservsCars)
//    {
//        if (car.ReturnDate.Hour == today.Hour 
//            && car.ReturnDate.Day == today.Day 
//            && car.ReturnDate.Month == today.Month 
//            && car.ReturnDate.Year == today.Year)
//            await _carServices.ReservCar(car.CarId);
//    }
//}