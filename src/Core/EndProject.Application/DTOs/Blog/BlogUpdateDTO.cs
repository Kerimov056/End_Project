using EndProject.Application.DTOs.BlogImage;

namespace EndProject.Application.DTOs.Blog;

public class BlogUpdateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<BlogImageUpdateDTO> BlogImageUpdateDTOs { get; set; }
}
