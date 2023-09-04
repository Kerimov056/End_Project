using EndProject.Application.DTOs.PickupLocation;
using EndProject.Application.DTOs.ReturnLocation;
using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.CarReservation;

public class AllCarReservation
{
    public IFormFile Image { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Number { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
    public string AppUserId { get; set; }
    public PickupLocationDTO? PickupLocation { get; set; } = null;
    public ReturnLocationDTO? ReturnLocation { get; set; } = null;
}
