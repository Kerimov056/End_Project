using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class CarType:BaseEntity  //sedan zad
{
    public string type { get; set; }
    //public List<TypeCategory> typeCategories { get; set; }
    public Guid CarId { get; set; }
    public Car Car { get; set; }
}

