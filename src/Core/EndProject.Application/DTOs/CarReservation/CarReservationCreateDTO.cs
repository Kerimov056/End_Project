using EndProject.Application.DTOs.PickupLocation;
using EndProject.Application.DTOs.ReturnLocation;
using EndProject.Domain.Enums.ReservationStatus;
using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.CarReservation;

public class CarReservationCreateDTO
{
    public IFormFile Image { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
    public ReservationStatus Status { get; set; }
    public string AppUserId { get; set; }
    public Guid CarId { get; set; }
    public PickupLocationDTO? PickupLocation { get; set; }
    public ReturnLocationDTO? ReturnLocation { get; set; }
    public Guid? ChauffeursId { get; set; }
}

