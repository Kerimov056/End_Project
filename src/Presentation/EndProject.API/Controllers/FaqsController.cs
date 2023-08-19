using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Advantage;
using EndProject.Application.DTOs.Faq;
using EndProjet.Persistance.Implementations.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FaqsController : ControllerBase
{
    private readonly IFaqServices _faqServices;

    public FaqsController(IFaqServices faqServices)
    => _faqServices = faqServices;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _faqServices.GetAllAsync();
        return Ok(Slider);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] FaqCreateDTO faqCreateDTO)
    {
        await _faqServices.CreateAsync(faqCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _faqServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _faqServices.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] FaqUpdateDTO faqUpdateDTO)
    {
        await _faqServices.UpdateAsync(id, faqUpdateDTO);
        return Ok();
    }
}
