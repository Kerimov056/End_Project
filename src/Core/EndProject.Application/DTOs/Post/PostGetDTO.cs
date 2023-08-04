using EndProject.Application.DTOs.Comments;

namespace EndProject.Application.DTOs.Post;

public class PostGetDTO
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public List<PostImageGetDTO> Images { get; set; }
    public string AppUserId { get; set; }
    public List<CommentGetDTO> commentGetDTOs { get; set; }
    public List<PostLikeGetDTO> postLikeGetDTOs { get; set; }

}
