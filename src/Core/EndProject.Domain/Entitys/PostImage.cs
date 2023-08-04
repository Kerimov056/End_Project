using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class PostImage:BaseEntity
{
    public Guid PostsId { get; set; }
    public Posts Posts { get; set; }
    public string imagePath { get; set; }
}
