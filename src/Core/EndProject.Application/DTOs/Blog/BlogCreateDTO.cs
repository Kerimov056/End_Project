using EndProject.Application.DTOs.BlogImage;
using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.Blog;

public class BlogCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<IFormFile> blogImages { get; set; }
}
