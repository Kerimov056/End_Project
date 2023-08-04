using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Comments:BaseEntity
{
    public string message { get; set; }
    public Guid PostsId { get; set; }
    public Posts Posts { get; set; }
}
