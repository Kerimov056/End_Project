using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Advantage;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdvantagesController : ControllerBase
{
    private readonly IAdvantageServices _advantageServices;

    public AdvantagesController(IAdvantageServices advantageServices) 
        => _advantageServices = advantageServices;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _advantageServices.GetAllAsync();
        return Ok(Slider);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AdvantageCreateDTO advantageCreateDTO)
    {
        await _advantageServices.CreateAsync(advantageCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _advantageServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _advantageServices.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, AdvantageUpdateDTO advantageUpdateDTO)
    {
        await _advantageServices.UpdateAsync(id, advantageUpdateDTO);
        return Ok();
    }

}
