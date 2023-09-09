using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class BlogImage:BaseEntity
{
    public byte[] imagePath { get; set; }
    public Guid BlogId { get; set; }
    public Blog Blog { get; set; }
}
