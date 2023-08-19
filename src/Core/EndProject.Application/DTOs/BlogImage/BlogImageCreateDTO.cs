using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.BlogImage;

public class BlogImageCreateDTO
{
    public IFormFile imagePath { get; set; }
    public Guid BlogId { get; set; }
}
