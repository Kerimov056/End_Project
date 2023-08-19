using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Car : BaseEntity
{
    public string Marka { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public bool isReserv { get; set; } = false;
    public CarType carType { get; set; }
    public CarCategory carCategory { get; set; }
    public List<CarImage>? carImages { get; set; }
    public List<CarTag>? carTags { get; set; }
    public List<CarComment>? Comments { get; set; }
    public List<CarReservation>? Reservations { get; set; }
    public List<OtherCarReservation>? OtherReservations { get; set; }

}
