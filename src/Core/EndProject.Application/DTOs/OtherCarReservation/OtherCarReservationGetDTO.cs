using EndProject.Domain.Enums.ReservationStatus;

namespace EndProject.Application.DTOs.OtherCarReservation;

public class OtherCarReservationGetDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public DateTime DTime { get; set; }
    public string PersonImage { get; set; }
    public string ImagePath { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
    public ReservationStatus Status { get; set; }
    public string AppUserId { get; set; }
    public Guid CarId { get; set; }
}
