using EndProject.Application.DTOs.PickupLocation;
using EndProject.Application.DTOs.ReturnLocation;
using EndProject.Domain.Enums.ReservationStatus;
using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.CarReservation;

public class CarReservationUpdateDTO
{
    public IFormFile ImagePath { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
    public ReservationStatus Status { get; set; }
    public string AppUserId { get; set; }
    public Guid CarId { get; set; }
    public ReturnLocationUpdateDTO PickupLocation { get; set; }
    public PickupLocationUpdateDTO ReturnLocation { get; set; }
    public Guid? ChauffeursId { get; set; }
}
