using Microsoft.AspNetCore.Identity;

namespace EndProject.Domain.Entitys.Identity;

public class AppUser:IdentityUser
{
    public bool IsActive { get; set; }
    public byte[]? ImagePath { get; set; }
    public string? FullName { get; set; }
    public DateTime RefreshTokenExpration { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? BirthDate { get; set; }
    public List<CarComment>? Comments { get; set; }
    public List<CarReservation>? Reservations { get; set; }
    public List<OtherCarReservation>? OtherReservations { get; set; }
    public Basket Basket { get; set; }
    public Wishlist Wishlist { get; set; }
}


