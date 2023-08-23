namespace EndProject.Application.DTOs.Like;

public class LikeCreateDTO
{
    public int LikeSum { get; set; }
    public string AppUserId { get; set; }
    public Guid CarCommentId { get; set; }
}
