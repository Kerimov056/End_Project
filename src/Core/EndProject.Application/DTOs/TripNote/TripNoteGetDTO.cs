namespace EndProject.Application.DTOs.TripNote;

public class TripNoteGetDTO
{
    public Guid Id { get; set; }
    public string Comment { get; set; }
    public DateTime CreateTripNote { get; set; }
    public Guid TripId { get; set; }
    public string UserName { get; set; }
    public string AppUserId { get; set; }
}
