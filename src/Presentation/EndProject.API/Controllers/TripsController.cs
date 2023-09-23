using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Trip;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly ITripServices _tripServices;

    public TripsController(ITripServices tripServices)
    => _tripServices = tripServices;


    [HttpGet]
    public async Task<IActionResult> GetAll(string AppUserId)
    {
        var trip = await _tripServices.GetAllAsync(AppUserId);
        return Ok(trip);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] TripCreateDTO tripCreateDTO)
    {
        await _tripServices.CreateAsync(tripCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var byTrip = await _tripServices.GetByIdAsync(id);
        return Ok(byTrip);
    }

    [HttpDelete("RemoveTrip")]
    public async Task<IActionResult> Remove([FromForm] TripRemoveDTO tripRemoveDTO)
    {
        await _tripServices.RemoveAsync(tripRemoveDTO);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] TripUpdateDTO tripUpdateDTO)
    {
        await _tripServices.UpdateAsync(id, tripUpdateDTO);
        return Ok();
    }

}
