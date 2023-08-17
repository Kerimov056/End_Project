using AutoMapper;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarType;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;
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
        var Slider = await _carServices.GetAllAsync();
        return Ok(Slider);
    }

    [HttpPost]
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
    public async Task<IActionResult> GetByType(string car)
    {
        var BySlider = await _carServices.GetByNameAsync(car);
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

}
