using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Comments;
using EndProject.Application.DTOs.Post;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    public CommentsController(ICommentService commentService) => _commentService = commentService;


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CommentCreateDTO commentCreateDTO)
    {
        await _commentService.AddAsync(commentCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var AllComment = await _commentService.GettAllAsync();
        return Ok(AllComment);
    }

    [HttpGet("{Id:Guid}")]
    public async Task<IActionResult> GetAll(Guid Id)
    {
        var ByComment = await _commentService.GetByIdAsync(Id);
        return Ok(ByComment);
    }

    [HttpDelete("{Id:Guid}")]
    public async Task<IActionResult> Remove(Guid Id)
    {
        await _commentService.RemoveAsync(Id);
        return Ok();
    }

    [HttpPut("{Id:Guid}")]
    public async Task<IActionResult> Update(Guid Id, [FromBody] CommentUpdateDTO commentUpdateDTO)
    {
        await _commentService.UpdateAsync(Id, commentUpdateDTO);
        return Ok();
    }
}
