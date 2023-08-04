using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class PostImage:BaseEntity
{
    public string imagePath { get; set; }
    public Guid PostsId { get; set; }
    public Posts Posts { get; set; }
}
