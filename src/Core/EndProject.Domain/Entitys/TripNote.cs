using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class TripNote : BaseEntity
{
    public string Comment { get; set; }
    public Guid TripId { get; set; }
    public Trip Trip { get; set; }
    public string UserName { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}
