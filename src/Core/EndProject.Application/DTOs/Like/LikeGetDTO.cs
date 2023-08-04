namespace EndProject.Application.DTOs.Like;

public class LikeGetDTO
{
    public Guid Id { get; set; }
    public int likeSum { get; set; }
    public Guid CommentId { get; set; }
    public Guid AppUserId { get; set; }

}
