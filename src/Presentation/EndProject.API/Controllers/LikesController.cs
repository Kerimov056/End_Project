using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Like;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{
    private readonly ILikeServices _likeServices;

    public LikesController(ILikeServices likeServices)
    =>   _likeServices = likeServices;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _likeServices.GetAllAsync();
        return Ok(Slider);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] LikeCreateDTO likeCreateDTO)
    {
        await _likeServices.CreateAsync(likeCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _likeServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _likeServices.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] LikeUpdateDTO likeUpdateDTO)
    {
        await _likeServices.UpdateAsync(id, likeUpdateDTO);
        return Ok();
    }
}
