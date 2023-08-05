using EndProject.Application.DTOs.Post_Tag;
using EndProject.Domain.Entitys;

namespace EndProject.Application.DTOs.Tag;

public class TagGetDTO
{
    public Guid Id { get; set; }
    public string Tag { get; set; }
    public List<Post_TagGetDTO> post_TagGetDTOs { get; set; }
}
