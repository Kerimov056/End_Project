using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaskerProductsController : ControllerBase
{
    private readonly IBasketProducServices _basketProducServices;

    public BaskerProductsController(IBasketProducServices basketProducServices)
     => _basketProducServices = basketProducServices;

    //[HttpPost]
    //public async Task<IActionResult> AllCarReservation([FromBody] CarReservationCreateDTO carReservationCreateDTO)
    //{
    //    await _basketProducServices.AllCreateAsync(carReservationCreateDTO);
    //    return StatusCode((int)HttpStatusCode.Created);
    //}
}
