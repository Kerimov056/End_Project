namespace EndProject.Application.DTOs.Post;

public class PostLikeCreateDTO
{
    public int likeSum { get; set; }
    public Guid PostId { get; set; }
    public string AppUserId { get; set; }
}
