using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class CarTag:BaseEntity
{
    public Guid CarId { get; set; }
    public Car Car { get; set; }
    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}
