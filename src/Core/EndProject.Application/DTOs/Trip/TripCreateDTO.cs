namespace EndProject.Application.DTOs.Trip;

public class TripCreateDTO
{
    public string? Image { get; set; }
    public string Destination { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
