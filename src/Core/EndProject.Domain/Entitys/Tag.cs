using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Tag:BaseEntity
{
    public string tag { get; set; }
    public List<CarTag> carTags { get; set; }
}
