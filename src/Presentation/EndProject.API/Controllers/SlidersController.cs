using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Slider;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net;
using System.Text;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SlidersController : ControllerBase
{
    private readonly ISliderService _sliderService;
    public SlidersController(ISliderService sliderService) => _sliderService = sliderService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _sliderService.GetAllAsync();

        return Ok(Slider);
    }
    
    [HttpGet("qrcode")]
    public async Task<IActionResult> GetQrCodeToProduct(Guid sliderId)
    {
        var data = await _sliderService.GetQRCOdoerSlider(sliderId);
        return File(data,"image/png");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] SliderCreateDTO sliderCreateDTO)
    {
        await _sliderService.CreateAsync(sliderCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _sliderService.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _sliderService.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] SliderUpdateDTO sliderUptadeDTO)
    {
        await _sliderService.UpdateAsync(id, sliderUptadeDTO);
        return Ok();
    }

}