using EndProject.Domain.Enums.ReservationStatus;
using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.OtherCarReservation;

public class OtherCarReservationUpdateDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public DateTime DTime { get; set; }
    public IFormFile PersonImage { get; set; }
    public IFormFile ImagePath { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
    public ReservationStatus Status { get; set; }
    public string AppUserId { get; set; }
    public Guid CarId { get; set; }
}
