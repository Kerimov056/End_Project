using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class CampaignStatistika : BaseEntity
{
    public int ReservationSum { get; set; } = 0;
    public string? CampaignName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime FinshTime { get; set; }
}
