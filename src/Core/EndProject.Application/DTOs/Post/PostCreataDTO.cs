using EndProject.Application.DTOs.Comments;

namespace EndProject.Application.DTOs.Post;

public class PostCreateDTO
{
    public string Message { get; set; }
    public List<PostImageCreateDTO> Images { get; set; }
    public string AppUserId { get; set; }
    public List<CommentCreateDTO> commentGetDTOs { get; set; }  
    public List<PostLikeCreateDTO> postLikeCreateDTOs { get; set; }   
}
