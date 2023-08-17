using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Location:BaseEntity
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
