using EndProject.Application.DTOs.BlogImage;
using EndProject.Domain.Entitys;

namespace EndProject.Application.DTOs.Blog;

public class BlogGetDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<BlogImageGetDTO> BlogImages { get; set; }
}
