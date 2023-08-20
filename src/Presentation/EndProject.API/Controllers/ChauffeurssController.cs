using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Chauffeurs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChauffeurssController : ControllerBase
{
    private readonly IChauffeursServices _chauffeursService;

    public ChauffeurssController(IChauffeursServices chauffeursService) => _chauffeursService = chauffeursService;
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _chauffeursService.GetAllAsync();
        return Ok(Slider);
    }

    [HttpGet("view")]
    public async Task<IActionResult> GetAlViewl()
    {
        var Slider = await _chauffeursService.ViewGetAll();
        return Ok(Slider);
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromForm] ChauffeursCreateDTO chauffeursCreateDTO)
    {
        await _chauffeursService.CreateAsync(chauffeursCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _chauffeursService.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _chauffeursService.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] ChauffeursUpdateDTO chauffeursUpdateDTO)
    {
        await _chauffeursService.UpdateAsync(id, chauffeursUpdateDTO);
        return Ok();
    }

    [HttpPut("isCheuffeursTrue")]
    public async Task<IActionResult> isCheuffeursTrue(Guid isCheuff)
    {
        await _chauffeursService.IsChauffeursTrue(isCheuff);
        return Ok();
    }

    [HttpPut("isCheuffeursFalse")]
    public async Task<IActionResult> isCheuffeursFalse(Guid isCheuff)
    {
        await _chauffeursService.IsChauffeursFalse(isCheuff);
        return Ok();
    }
}
