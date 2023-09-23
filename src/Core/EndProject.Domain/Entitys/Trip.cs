using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Trip:BaseEntity
{
    public string? Image { get; set; }
    public string Destination { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
