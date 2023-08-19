using EndProject.Application.DTOs.Reservation;

namespace EndProject.Application.DTOs.Chauffeurs;

public class ChauffeursGetDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string ImagePath { get; set; }
    public double Price { get; set; }
    public bool isChauffeurs { get; set; }
    public ReservationGetDTO CarReservation { get; set; }
}
