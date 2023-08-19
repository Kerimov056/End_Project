using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Application.DTOs.Slider;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarReservationsController : ControllerBase
{
    private readonly ICarReservationServices _carReservationServices;
    public CarReservationsController(ICarReservationServices carReservationServices) => _carReservationServices = carReservationServices;


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _carReservationServices.GetAllAsync();
        return Ok(Slider);
    }

    [HttpGet("UserId")]
    public async Task<IActionResult> YouGetAll(string Id)
    {
        var Slider = await _carReservationServices.YouGetAllAsync(Id);
        return Ok(Slider);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CarReservationCreateDTO carReservationCreateDTO)
    {
        await _carReservationServices.CreateAsync(carReservationCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _carReservationServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _carReservationServices.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] CarReservationUpdateDTO carReservationUpdateDTO)
    {
        await _carReservationServices.UpdateAsync(id, carReservationUpdateDTO);
        return Ok();
    }

    [HttpPut("Confirmed")]
    public async Task<IActionResult> UptadeStatus(Guid Id)
    {
        await _carReservationServices.StatusConfirmed(Id);
        return Ok();
    }
}
