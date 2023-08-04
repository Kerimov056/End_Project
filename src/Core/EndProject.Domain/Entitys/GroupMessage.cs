using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class GroupMessage:BaseEntity
{
    public string message { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid GroupId { get; set; }
    public Group Group { get; set; }

}
