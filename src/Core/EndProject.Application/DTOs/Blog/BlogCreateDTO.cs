using EndProject.Application.DTOs.BlogImage;

namespace EndProject.Application.DTOs.Blog;

public class BlogCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<BlogImageCreateDTO> BlogImageCreateDTOs { get; set; }
}
