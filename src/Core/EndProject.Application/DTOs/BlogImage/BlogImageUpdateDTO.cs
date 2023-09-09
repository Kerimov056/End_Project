using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.BlogImage;

public class BlogImageUpdateDTO
{
    public IFormFile imagePath { get; set; }
    public Guid BlogId { get; set; }
}
