using EndProject.Application.DTOs.CarComment;
using EndProject.Application.DTOs.CarImage;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Application.DTOs.CarType;
using EndProject.Application.DTOs.Category;
using EndProject.Application.DTOs.OtherCarReservation;
using EndProject.Application.DTOs.Reservation;
using EndProject.Domain.Entitys;

namespace EndProject.Application.DTOs.Car;

public class CarGetDTO
{
    public Guid Id { get; set; }
    public string Marka { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public bool isReserv { get; set; }
    public CarTypeGetDTO CarType { get; set; } 
    public CarCategoryGetDTO CarCategory { get; set; }
    public List<CarCommentGetDTO> carCommentGetDTO { get; set; }
    public List<CarImageGetDTO> CarImages { get; set; } 
    public List<string> CarTags { get; set; } 
    public Guid? ReservationsId { get; set; }
    public List<OtherCarReservationGetDTO>? OtherReservations { get; set; }

}
