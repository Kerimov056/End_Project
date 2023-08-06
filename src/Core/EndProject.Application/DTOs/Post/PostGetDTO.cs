using EndProject.Application.DTOs.Comments;
using EndProject.Application.DTOs.NewTag;
using EndProject.Application.DTOs.Post_Tag;

namespace EndProject.Application.DTOs.Post;

public class PostGetDTO
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public List<PostImageGetDTO> Images { get; set; }
    public string AppUserId { get; set; }
    public List<NewTagGetDTO> NewTagGetDTOs { get; set; }
    public List<CommentGetDTO> commentGetDTOs { get; set; }
    public List<PostLikeGetDTO> postLikeGetDTOs { get; set; }
    public List<Post_TagGetDTO> Post_TagGetDTO { get; set; }

}
