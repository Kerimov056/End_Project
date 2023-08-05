using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.Post;

public class PostImageCreateDTO
{
    public IFormFile ImagePath { get; set; }
    public Guid PostsId { get; set; }
}
