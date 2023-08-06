using EndProject.Application.DTOs.Like;

namespace EndProject.Application.DTOs.Comments;

public class CommentUpdateDTO
{
    public string Comment { get; set; }
    public Guid PostId { get; set; }
    public string AppUserId { get; set; }
    //public List<LikeUpdateDTO> LikeUpdateDto { get; set; }
}
