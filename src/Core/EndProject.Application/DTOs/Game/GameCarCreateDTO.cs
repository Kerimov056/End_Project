namespace EndProject.Application.DTOs.Game;

public class GameCarCreateDTO
{
    public string AppUserId { get; set; }
    public Guid CarId { get; set; }
    public string Password { get; set; }
    public bool Win { get; set; } = true;
}
