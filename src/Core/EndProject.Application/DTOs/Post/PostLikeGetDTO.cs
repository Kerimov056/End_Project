namespace EndProject.Application.DTOs.Post;

public class PostLikeGetDTO
{
    public int Id { get; set; }
    public int likeSum { get; set; }
    public Guid PostId { get; set; }
    public string AppUserId { get; set; }
}
