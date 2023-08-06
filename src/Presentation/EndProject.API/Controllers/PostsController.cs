using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Post;
using EndProject.Domain.Entitys;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;
    public PostsController(IPostService postService) => _postService = postService;


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostCreateDTO postCreateDTO)
    {
        await _postService.AddAsync(postCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var AllPost = await _postService.GettAllAsync();
        return Ok(AllPost);
    }

    [HttpGet("{Id:Guid}")]
    public async Task<IActionResult> GetAll(Guid Id)
    {
        var ByPost = await _postService.GetByIdAsync(Id);
        return Ok(ByPost);
    }

    [HttpDelete("{Id:Guid}")]
    public async Task<IActionResult> Remove(Guid Id)
    {
        await _postService.RemoveAsync(Id);
        return Ok();
    }

    [HttpPut("{Id:Guid}")]
    public async Task<IActionResult> Update(Guid Id, [FromBody] PostCreateDTO postCreateDTO )
    {
        await _postService.UpdateAsync(Id, postCreateDTO);
        return Ok();
    }

    [HttpGet("detail")]
    public async Task<IActionResult> GetPostWithDetail()
    {
        var Postss = await _postService.GetAllPostsWithDetails();
        return Ok(Postss);
    }
}
