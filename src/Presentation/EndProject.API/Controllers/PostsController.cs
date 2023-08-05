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
}
