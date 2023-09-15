namespace EndProject.Application.DTOs.CampaignStatistika;

public class CampaignStatistikaCreateDTO
{
    public int ReservationSum { get; set; } = 0;
    public string CampaignName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime FinshTime { get; set; }
    public List<Guid>? ReservationId { get; set; }
}
