using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.TripNote;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripNotesController : ControllerBase
    {
        private readonly ITripNoteServices _tripNoteServices;

        public TripNotesController(ITripNoteServices tripNoteServices)
         =>   _tripNoteServices = tripNoteServices;

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid TripId)
        {
            var tripNotes = await _tripNoteServices.GetAllAsync(TripId);
            return Ok(tripNotes);
        }

        [HttpPost("TripPost")]
        public async Task<IActionResult> Post([FromForm] TripNoteCreateDTO tripNoteCreateDTO)
        {
            await _tripNoteServices.CreateAsync(tripNoteCreateDTO);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ByTrip = await _tripNoteServices.GetByIdAsync(id);
            return Ok(ByTrip);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Remove(Guid id, string AppUserId)
        {
            await _tripNoteServices.RemoveAsync(id, AppUserId);
            return Ok();
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Uptade(Guid id, [FromForm] TripNoteUpdateDTO tripNoteUpdateDTO)
        {
            await _tripNoteServices.UpdateAsync(id, tripNoteUpdateDTO);
            return Ok();
        }
    }
}
