using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class GameCar : BaseEntity
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid CarId { get; set; }
    public string Password { get; set; }
    public bool Win { get; set; } = false;
}
