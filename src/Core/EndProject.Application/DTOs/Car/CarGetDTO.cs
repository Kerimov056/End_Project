using EndProject.Application.DTOs.CarType;
using EndProject.Application.DTOs.Category;
using EndProject.Application.DTOs.Reservation;

namespace EndProject.Application.DTOs.Car;

public class CarGetDTO
{
    public int CarId { get; set; }
    public string Marka { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public CarTypeGetDTO CarType { get; set; } 
    public CarCategoryGetDTO CarCategory { get; set; } 
    public List<string> CarImages { get; set; } 
    public List<string> CarTags { get; set; } 
    public List<ReservationGetDTO> Reservations { get; set; }
}
