using EndProject.Application.DTOs.CarReservation;

namespace EndProject.Application.DTOs.CampaignStatistika;

public class CampaignStatistikaGetDTO
{
    public Guid Id { get; set; }
    public int ReservationSum { get; set; }
    public string? CampaignName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime FinshTime { get; set; }
}
