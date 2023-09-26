using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Enums.Role;

namespace EndProject.Domain.Entitys;

public class ShareTrip : BaseEntity
{
    public string Email { get; set; }
    public string? Message { get; set; }
    public Guid TripId { get; set; }
    public Trip Trip { get; set; }
    public TripRole TripRole { get; set; }
    public string? AppUserId { get; set; }
}
