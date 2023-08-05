using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Tags:BaseEntity
{
    public string Tag { get; set; }
    public List<Post_Tag> Post_Tags { get; set; }
}
