using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Communication;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommunicationsController : ControllerBase
{
    private readonly ICommunicationServices _communicationServices;
    public CommunicationsController(ICommunicationServices communicationServices) => _communicationServices = communicationServices;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var allCommunications = await _communicationServices.GetAllAsync();
        return Ok(allCommunications);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CommunicationCreateDTO communicationCreateDTO)
    {
        await _communicationServices.CreateAsync(communicationCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _communicationServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _communicationServices.RemoveAsync(id);
        return Ok();
    }
}
