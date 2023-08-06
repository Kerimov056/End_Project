using EndProject.Application.DTOs.Comments;
using EndProject.Application.DTOs.NewTag;
using EndProject.Application.DTOs.Post_Tag;

namespace EndProject.Application.DTOs.Post;

public class PostCreateDTO
{
    public string Message { get; set; }
    public List<PostImageCreateDTO> Images { get; set; }
    public List<NewTagCreateDTO> NewTagCreateDTOs { get; set; }
    public Guid tagId { get; set; }
}



//public string AppUserId { get; set; }
//public List<NewTagCreateDTO> Tags { get; set; }

//public List<CommentCreateDTO> commentGetDTOs { get; set; }  
//public List<PostLikeCreateDTO> postLikeCreateDTOs { get; set; }
//public List<Post_TagCreateDTO> MyProperty { get; set; }