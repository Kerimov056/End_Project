using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
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

    [HttpGet("resercPeddingCount")]
    public async Task<IActionResult> GetReservPeddingCount()
    {
        var peddingCount = await _carReservationServices.GetPeddingCountAsync();
        return Ok(peddingCount);
    }

    [HttpGet("resercPedding")]
    public async Task<IActionResult> GetReservPedding()
    {
        var Slider = await _carReservationServices.IsResevPedingGetAll();
        return Ok(Slider);
    }

    [HttpGet("ReservConfirmedCount")] 
    public async Task<IActionResult> GetReservConfirmed()
    {
        var reserv = await _carReservationServices.GetConfirmedCountAsync();
        return Ok(reserv);
    }
    [HttpGet("IsResevConfirmedGetAll")] 
    public async Task<IActionResult> ReservGetAllConfirmed()
    {
        var Slider = await _carReservationServices.IsResevConfirmedGetAll();
        return Ok(Slider);
    }

    [HttpGet("ReservComplatedCount")]
    public async Task<IActionResult> GetReservComplated()
    {
        var reserv = await _carReservationServices.GetCompletedCountAsync();
        return Ok(reserv);
    }

    
    [HttpGet("IsResevComplatedGetAll")]
    public async Task<IActionResult> ReservGetAllComplated()
    {
        var Slider = await _carReservationServices.IsResevComplatedGetAll();
        return Ok(Slider);
    }

    [HttpGet("IsResevCanceledCount")]
    public async Task<IActionResult> ReservGetAllCanceledCount()
    {
        var reservCount = await _carReservationServices.GetCanceledCountAsync();
        return Ok(reservCount); 
    }

    [HttpGet("IsResevCanceledGetAll")]
    public async Task<IActionResult> ReservGetAllCanceled()
    {
        var Slider = await _carReservationServices.IsResevCanceledGetAll();
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
        Console.WriteLine(carReservationCreateDTO.AppUserId
           , carReservationCreateDTO.CarId
           , carReservationCreateDTO.Number
           , carReservationCreateDTO.Email,
           "TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT" +
           "TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT" +
           "");
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
