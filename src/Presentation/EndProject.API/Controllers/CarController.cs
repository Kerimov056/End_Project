using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Car;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarServices _carServices;

    public CarController(ICarServices carServices) => _carServices = carServices;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var car = await _carServices.GetAllAsync();
        return Ok(car);
    } 
    
    [HttpGet("GetAllCar")]
    public async Task<IActionResult> GetAllCar()
    {
        var car = await _carServices.GetAllCarAsync();
        return Ok(car);
    }

    [HttpGet("AllMarka")]
    public async Task<IActionResult> GetAllMarka()
    {
        var AllMarka = await _carServices.GetAllCarMarka();
        return Ok(AllMarka);
    }

    [HttpGet("AllModel")]
    public async Task<IActionResult> GetAllModel()
    {
        var AllModel = await _carServices.GetAllCarModel();
        return Ok(AllModel);
    }

    [HttpGet("Count")]
    public async Task<IActionResult> GetCarCount()
    {
        var count = await _carServices.GetCarCountAsync();
        return Ok(count);
    }

    [HttpGet("ReservCarCount")]
    public async Task<IActionResult> GetReservCarCount()
    {
        var count = await _carServices.GetReservCarCountAsync();
        return Ok(count);
    }

    [HttpPost("postCar")]
    public async Task<IActionResult> Post([FromForm] CarCreateDTO carCreateDTO)
    {
        await _carServices.CreateAsync(carCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _carServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpGet("car")]
    public async Task<IActionResult> GetByType(string? car,string? model)
    {
        var BySlider = await _carServices.GetByNameAsync(car,model);
        return Ok(BySlider);
    }

    [HttpGet("searchCar")]
    public async Task<IActionResult> GetSearchCars(string? category,
                                                   string? type,
                                                   string? marka,
                                                   string? model,
                                                   decimal? minPrice,
                                                   decimal? maxPrice)
    {
        var BySlider = await _carServices.GetSearchCar(category, type, marka, model, minPrice, maxPrice);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _carServices.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] CarUpdateDTO carUpdateDTO)
    {
        await _carServices.UpdateAsync(id, carUpdateDTO);
        return Ok();
    }
    [HttpPut("IsReservTrue")]
    public async Task<IActionResult> UptadeReservTrue(Guid id)
    {
        await _carServices.ReservCarTrue(id);
        return Ok();
    }

    [HttpPut("IsReservFalse")]
    public async Task<IActionResult> UptadeReservFalse(Guid id)
    {
        await _carServices.ReservCarFalse(id);
        return Ok();
    }


    [HttpPost("Campaigns")]  //d2cb6d2a-0d22-4437-a8fa-29fdaf8b1341
    public async Task<IActionResult> CarCampaigns([FromForm] CarCampaignsDTO carCampaignsDTO)
    {
        await _carServices.Campaigns(carCampaignsDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }

}
