using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Blog:BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<BlogImage> BlogImages { get; set; }
}
