using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarComment;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarCommetsController : ControllerBase
{
    private readonly ICarCommentServices _carCommentServices;
    public CarCommetsController(ICarCommentServices carCommentServices) => _carCommentServices = carCommentServices;

    [HttpGet]
    public async Task<IActionResult> GetAll(Guid CarId)
    {
        var Slider = await _carCommentServices.GetAllAsync(CarId);
        return Ok(Slider);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CarCommentCreateDTO carCommentCreateDTO)
    {
        await _carCommentServices.CreateAsync(carCommentCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _carCommentServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _carCommentServices.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] CarCommentUpdateDTO carCommentUpdateDTO)
    {
        await _carCommentServices.UpdateAsync(id, carCommentUpdateDTO);
        return Ok();
    }
}
