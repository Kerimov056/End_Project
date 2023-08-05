namespace EndProject.Application.DTOs.Like;

public class LikeUpdateDTO
{
    public int likeSum { get; set; }
    public Guid CommentId { get; set; }
    public string AppUserId { get; set; }
}
