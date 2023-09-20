using EndProject.Application.Abstraction.Services.Game;
using EndProject.Application.DTOs.Game;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameCarsController : ControllerBase
{
    private readonly IGameCarServices _gameCarServices;

    public GameCarsController(IGameCarServices gameCarServices) => _gameCarServices = gameCarServices;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var gameCarprofile = await _gameCarServices.GetAllAsync();

        return Ok(gameCarprofile);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] GameCarCreateDTO gameCarCreateDTO)
    {
        await _gameCarServices.CreateAsync(gameCarCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var byGameCarprofile = await _gameCarServices.GetByIdAsync(id);
        return Ok(byGameCarprofile);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _gameCarServices.RemoveAsync(id);
        return Ok();
    }
}
