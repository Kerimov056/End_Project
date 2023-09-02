using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EndProject.Application.DTOs.Blog;

public class BlogCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public List<IFormFile> blogImages { get; set; }
}

