using EndProject.Domain.Enums.Role;

namespace EndProject.Application.DTOs.ShareTrip;

public class ShareTripGetDTO
{
    public string Email { get; set; }
    public Guid Id { get; set; }
    public Guid TripId { get; set; }
    public TripRole TripRole { get; set; }
    public string? AppUserId { get; set; }
}
