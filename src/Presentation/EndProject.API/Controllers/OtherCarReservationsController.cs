using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.OtherCarReservation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OtherCarReservationsController : ControllerBase
{
    private readonly IOtherCarReservationServices _therCarReservationServices;

    public OtherCarReservationsController(IOtherCarReservationServices therCarReservationServices) 
        => _therCarReservationServices = therCarReservationServices;


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _therCarReservationServices.GetAllAsync();
        return Ok(Slider);
    }

    [HttpGet("IsResevConfirmedGetAll")]
    public async Task<IActionResult> ReservGetAllConfirmed()
    {
        var Slider = await _therCarReservationServices.IsResevConfirmedGetAll();
        return Ok(Slider);
    }

    [HttpGet("IsResevComplatedGetAll")]
    public async Task<IActionResult> ReservGetAllComplated()
    {
        var Slider = await _therCarReservationServices.IsResevComplatedGetAll();
        return Ok(Slider);
    }

    [HttpGet("IsResevCanceledGetAll")]
    public async Task<IActionResult> ReservGetAllCanceled()
    {
        var Slider = await _therCarReservationServices.IsResevCanceledGetAll();
        return Ok(Slider);
    }


    [HttpGet("UserId")]
    public async Task<IActionResult> YouGetAll(string Id)
    {
        var Slider = await _therCarReservationServices.YouGetAllAsync(Id);
        return Ok(Slider);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] OtherCarReservationCreateDTO otherCarReservationCreateDTO)
    {
        await _therCarReservationServices.CreateAsync(otherCarReservationCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _therCarReservationServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _therCarReservationServices.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] OtherCarReservationUpdateDTO otherCarReservationUpdateDTO)
    {
        await _therCarReservationServices.UpdateAsync(id, otherCarReservationUpdateDTO);
        return Ok();
    }

    [HttpPut("Confirmed")]
    public async Task<IActionResult> UptadeStatus(Guid Id)
    {
        await _therCarReservationServices.StatusConfirmed(Id);
        return Ok();
    }
}
