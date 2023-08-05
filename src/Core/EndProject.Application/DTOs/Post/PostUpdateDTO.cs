using EndProject.Application.DTOs.Comments;
using EndProject.Application.DTOs.Post_Tag;

namespace EndProject.Application.DTOs.Post;

public class PostUpdateDTO
{
    public string Message { get; set; }
    public List<PostImageUpdateDTO> Images { get; set; }
    public string AppUserId { get; set; }
    public List<CommentUpdateDTO> commentGetDTOs { get; set; }
    public List<PostLikeUpdateDTO> postLikeGetDTOs { get; set; }
    public List<Post_TagUpdateDTO> MyProperty { get; set; }

}

