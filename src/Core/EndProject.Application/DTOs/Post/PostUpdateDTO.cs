using EndProject.Application.DTOs.Comments;

namespace EndProject.Application.DTOs.Post;

public class PostUpdateDTO
{
    public string Message { get; set; }
    public List<PostImageUpdateDTO> Images { get; set; }
    public string AppUserId { get; set; }
    public List<CommentUpdateDTO> commentGetDTOs { get; set; }
    public List<PostLikeUpdateDTO> postLikeGetDTOs { get; set; }
}


//PostUpdateDTO
//PostImageUpdateDTO
//CommentUpdateDTO
//LikeUpdateDTO
//PostLikeUpdateDTO