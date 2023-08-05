using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Domain.Entitys;

public class NewTag:BaseEntity
{
    public string Tag { get; set; }
    public Guid PostsId { get; set; }
    public Posts Posts { get; set; }
}
