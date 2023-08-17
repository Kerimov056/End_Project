using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Enums.ReservationStatus;

namespace EndProject.Domain.Entitys;

public class CarReservation:BaseEntity
{
    public DateTime ReservationDate { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
    public Guid PickupLocationId { get; set; } 
    public Guid ReturnLocationId { get; set; } 
    public ReservationStatus Status { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid CarId { get; set; }
    public Car Car { get; set; }
    public Location PickupLocation { get; set; }
    public Location ReturnLocation { get; set; }
}
