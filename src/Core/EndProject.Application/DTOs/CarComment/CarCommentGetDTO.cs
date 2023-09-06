namespace EndProject.Application.DTOs.CarComment;

public class CarCommentGetDTO
{
    public Guid Id { get; set; }
    public string Comment { get; set; }
    public Guid CarId { get; set; }
    public string AppUserId { get; set; }
    public string UserName { get; set; }
    public int LikeSum { get; set; }
}
