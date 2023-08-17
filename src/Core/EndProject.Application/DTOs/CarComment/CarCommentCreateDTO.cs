namespace EndProject.Application.DTOs.CarComment;

public class CarCommentCreateDTO
{
    public string Comment { get; set; }
    public Guid CarId { get; set; }
    public Guid AppUserId { get; set; }
}
