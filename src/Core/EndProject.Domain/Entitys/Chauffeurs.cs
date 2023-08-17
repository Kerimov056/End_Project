using EndProject.Domain.Entitys.Common;

namespace EndProject.Domain.Entitys;

public class Chauffeurs:BaseEntity
{
    public string Name { get; set; }
    public string Number { get; set; }
    public string imagePath { get; set; }
    public CarReservation? CarReservation { get; set; }
}
