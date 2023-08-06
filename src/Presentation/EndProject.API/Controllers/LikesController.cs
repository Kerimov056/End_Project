using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Post;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{
    private readonly ILikeService _likeService;
    public LikesController(ILikeService likeService) => _likeService = likeService;


    [HttpPost]
    public async Task<IActionResult> Post(string userId, Guid commentId)
    {
        await _likeService.LikeCommentAsync(userId,commentId);
        return StatusCode((int)HttpStatusCode.Created);
    }
    
    [HttpGet("{commentId:Guid}")]
    public async Task<IActionResult> GetLikeCountForCommentt(Guid commentId)
    {
        var CommentLikeCount = await _likeService.GetLikeCountForComment(commentId);
        return Ok(CommentLikeCount);
    }

    [HttpDelete]
    public async Task<IActionResult> LikeRemove(string userId, Guid commentId)
    {
        await _likeService.UnlikeCommentAsync(userId, commentId);
        return StatusCode((int)HttpStatusCode.Created);
    }

}
