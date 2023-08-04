using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Group:BaseEntity
{
    public string GroupName { get; set; }
    public List<Group_User> group_Users { get; set; }
    public List<GroupMessage> groupMessages { get; set; }
}
