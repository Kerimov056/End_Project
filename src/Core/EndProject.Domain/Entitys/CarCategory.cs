using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class CarCategory:BaseEntity
{
    public string Category { get; set; }
    //public List<TypeCategory> typeCategories { get; set; }
    public Guid CarId { get; set; }
    public Car Car { get; set; }
}

