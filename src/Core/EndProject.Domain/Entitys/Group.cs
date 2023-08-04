using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class Group:BaseEntity
{
    public string GroupName { get; set; }
    public List<AppUser> AppUsers { get; set; }
}
