using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.ShareTrip;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShareTripsController : ControllerBase
{
    private readonly IShareTripServices _shareTripService;

    public ShareTripsController(IShareTripServices shareTripService)
    => _shareTripService = shareTripService;



    [HttpGet]
    public async Task<IActionResult> GetAll(Guid tripId)
    {
        var shareTrip = await _shareTripService.GetAllAsync(tripId);
        return Ok(shareTrip);
    }
    
    [HttpGet("Contributors")]
    public async Task<IActionResult> GetAllContributorsShareTrip(Guid tripId)
    {
        var shareTripContributors = await _shareTripService.GetAllContributorsUser(tripId);
        return Ok(shareTripContributors);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm]ShareTripCreateDTO shareTripCreateDTO)
    {
        await _shareTripService.CreateAsync(shareTripCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _shareTripService.GetByIdAsync(id);
        return Ok(BySlider);
    }


    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _shareTripService.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, ShareTripUpdateDTO shareTripUpdateDTO )
    {
        await _shareTripService.UpdateAsync(id, shareTripUpdateDTO);
        return Ok();
    }
}

