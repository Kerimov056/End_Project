using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Blog;
using EndProject.Application.DTOs.Slider;
using EndProjet.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsController : ControllerBase
{
    private readonly IBlogService _blogService;
    private readonly IBlogImageServices _blogImageServices;

    public BlogsController(IBlogService blogService, IBlogImageServices blogImageServices)
    {
        _blogService = blogService;
        _blogImageServices = blogImageServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _blogService.GetAllAsync();
        return Ok(Slider);
    }

    [HttpGet("AllBlogImage")]
    public async Task<IActionResult> BlogAllImage(Guid blogId)
    {
        var blogImage = await _blogImageServices.GetAllBlogIdAsync(blogId);
        return Ok(blogImage);
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromForm] BlogCreateDTO blogCreateDTO)
    {
        await _blogService.CreateAsync(blogCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _blogService.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _blogService.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] BlogUpdateDTO blogUpdateDTO)
    {
        await _blogService.UpdateAsync(id, blogUpdateDTO);
        return Ok();
    }
}
