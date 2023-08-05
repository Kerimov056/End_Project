using EndProject.Application.DTOs.Post_Tag;

namespace EndProject.Application.DTOs.Tag;

public class TagCreateDTO
{
    public string Tag { get; set; }
    public List<Post_TagCreateDTO> post_TagCreateDTOs { get; set; }
}
