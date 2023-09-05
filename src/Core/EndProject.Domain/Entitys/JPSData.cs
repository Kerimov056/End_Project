using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class JPSData:BaseEntity
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    //Relationship
    public Guid CarId { get; set; }
    public Car Car { get; set; }
}
