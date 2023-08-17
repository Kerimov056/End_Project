using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class TypeCategory:BaseEntity
{
    public Guid typeId { get; set; }
    public CarType type { get; set; }
    public Guid categoryId { get; set; }
    public CarCategory category { get; set; }
}
