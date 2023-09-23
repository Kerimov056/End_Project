using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class Trip:BaseEntity
{
    public string? Image { get; set; }
    public string Destination { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double? TripLatitude { get; set; }
    public double? TripLongitude { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
