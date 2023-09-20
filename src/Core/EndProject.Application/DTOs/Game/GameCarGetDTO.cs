using EndProject.Domain.Entitys.Identity;

namespace EndProject.Application.DTOs.Game;

public class GameCarGetDTO
{
    public Guid Id { get; set; }
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public Guid CarId { get; set; }
    public string Password { get; set; }
    public bool Win { get; set; } = false;
}
