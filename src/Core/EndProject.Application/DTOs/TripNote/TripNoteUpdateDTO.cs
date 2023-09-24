namespace EndProject.Application.DTOs.TripNote;

public class TripNoteUpdateDTO
{
    public string Comment { get; set; }
    public DateTime CreateTripNote { get; set; } = DateTime.Now;
    public Guid TripId { get; set; }
    public string UserName { get; set; }
    public string AppUserId { get; set; }
}
