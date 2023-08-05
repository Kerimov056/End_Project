using EndProject.Application.DTOs.Post_Tag;

namespace EndProject.Application.DTOs.Tag;

public class TagUpdateDTO
{
    public string Tag { get; set; }
    public List<Post_TagUpdateDTO> post_TagUpdateDTOs { get; set; }
}
