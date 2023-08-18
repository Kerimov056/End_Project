using EndProject.Domain.Enums.ReservationStatus;

namespace EndProject.Application.DTOs.CarReservation;

public class CarReservationCreateDTO
{
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
    public ReservationStatus Status { get; set; }
    public string AppUserId { get; set; }
    public Guid CarId { get; set; }
    public Guid? PickupLocationId { get; set; }
    public Guid? ReturnLocationId { get; set; }
    public Guid? ChauffeursId { get; set; }
}
