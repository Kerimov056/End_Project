using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Post_Tag:BaseEntity
{
    public Guid PostId { get; set; }
    public Posts Posts { get; set; }
    public Guid TagsId { get; set; }
    public Tags Tags { get; set; }
}
