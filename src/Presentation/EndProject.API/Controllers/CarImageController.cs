using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarImage;
using EndProject.Application.DTOs.Slider;
using EndProjet.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarImageController : ControllerBase
{
    private readonly ICarImageServices _carImageServices;

    public CarImageController(ICarImageServices carImageServices) =>  _carImageServices = carImageServices;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _carImageServices.GetAllAsync();
        return Ok(Slider);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CarImageCreateDTO carImageCreateDTO)
    {
        await _carImageServices.CreateAsync(carImageCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _carImageServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _carImageServices.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] CarImageUpdateDTO carImageUpdateDTO)
    {
        await _carImageServices.UpdateAsync(id, carImageUpdateDTO);
        return Ok();
    }
}
