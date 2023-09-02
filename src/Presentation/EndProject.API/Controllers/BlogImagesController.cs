using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Blog;
using EndProject.Application.DTOs.BlogImage;
using EndProjet.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogImagesController : ControllerBase
{
    private readonly IBlogImageServices _blogImageServices;

    public BlogImagesController(IBlogImageServices blogImageServices)
    =>   _blogImageServices = blogImageServices;

    [HttpPost]
    public async Task<IActionResult> Post(BlogImageCreateDTO blogImageCreateDTO)
    {
        await _blogImageServices.CreateAsync(blogImageCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }
}
