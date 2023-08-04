using EndProject.Application.DTOs.Like;

namespace EndProject.Application.DTOs.Comments;

public class CommentGetDTO
{
    public Guid Id { get; set; }
    public string Comment { get; set; }
    public Guid PostId { get; set; }
    public string AppUserId { get; set; }
    public List<LikeGetDTO> LikeGetDto { get; set; }
}
