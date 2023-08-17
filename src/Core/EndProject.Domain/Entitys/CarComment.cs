using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class CarComment:BaseEntity
{
    public string Comment { get; set; }
    public Guid CarId { get; set; }
    public Car Car { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
