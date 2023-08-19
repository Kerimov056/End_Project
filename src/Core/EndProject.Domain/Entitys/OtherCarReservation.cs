using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Enums.ReservationStatus;

namespace EndProject.Domain.Entitys;

public class OtherCarReservation:BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public DateTime DateOfYear { get; set; }
    public string PersonImage { get; set; }
    public string ImagePath { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
    public ReservationStatus Status { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid CarId { get; set; }
    public Car Car { get; set; }
}
