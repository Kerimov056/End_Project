using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.PickupLocation;
using EndProject.Application.DTOs.ReturnLocation;
using EndProject.Domain.Enums.ReservationStatus;

namespace EndProject.Application.DTOs.CarReservation;

public class CarReservationGetDTO
{
    public Guid Id { get; set; }
    public string ImagePath { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
    public ReservationStatus Status { get; set; }
    public string AppUserId { get; set; }
    public Guid CarId { get; set; }
    public CarGetDTO ReservCar { get; set; }
    public PickupLocationGetDTO PickupLocation { get; set; }
    public ReturnLocationGetDTO ReturnLocation { get; set; }
    public Guid? ChauffeursId { get; set; }
}
