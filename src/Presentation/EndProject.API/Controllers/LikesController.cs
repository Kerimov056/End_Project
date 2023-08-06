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

    [HttpGet]
    public async Task<IActionResult> GetUserLikeAll(Guid commentId)
    {
        var getUserLikeAll = await _likeService.GetUsersWhoLikedComment(commentId);
        return Ok(getUserLikeAll);
    } 
    
    [HttpGet("{commentId:Guid}")]
    public async Task<IActionResult> GetLikeCountForCommentt(Guid commentId)
    {
        var CommentLikeCount = await _likeService.GetLikeCountForComment(commentId);
        return Ok(CommentLikeCount);
    }

    //[HttpGet("{Id:Guid}")]
    //public async Task<IActionResult> GetAll(Guid Id)
    //{
    //    var ByPost = await _likeService.GetByIdAsync(Id);
    //    return Ok(ByPost);
    //}

    //[HttpDelete("{Id:Guid}")]
    //public async Task<IActionResult> Remove(Guid Id)
    //{
    //    await _likeService.RemoveAsync(Id);
    //    return Ok();
    //}

    //[HttpPut("{Id:Guid}")]
    //public async Task<IActionResult> Update(Guid Id, [FromBody] PostCreateDTO postCreateDTO)
    //{
    //    await _likeService.UpdateAsync(Id, postCreateDTO);
    //    return Ok();
    //}
}
