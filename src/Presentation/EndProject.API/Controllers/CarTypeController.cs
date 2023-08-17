using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarType;
using EndProject.Application.DTOs.Slider;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarTypeController : ControllerBase
{
    private readonly ICarTypeService _carTypeService;

    public CarTypeController(ICarTypeService carService) => _carTypeService = carService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _carTypeService.GetAllAsync();
        return Ok(Slider);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CarTypeCreateDTO carTypeCreateDTO)
    {
        await _carTypeService.CreateAsync(carTypeCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _carTypeService.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpGet("type")]
    public async Task<IActionResult> GetByType(string type)
    {
        var BySlider = await _carTypeService.GetByNameAsync(type);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _carTypeService.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] CarTypeUpdateDTO carTypeUpdateDTO)
    {
        await _carTypeService.UpdateAsync(id, carTypeUpdateDTO);
        return Ok();
    }
}
