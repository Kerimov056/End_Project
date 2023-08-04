using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class Group_User:BaseEntity
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

}
