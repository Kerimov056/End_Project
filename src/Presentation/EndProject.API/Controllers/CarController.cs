using AutoMapper;
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
    //[HttpGet]
    //public async Task<IActionResult> GetAll()
    //{
    //    var Slider = await _carService.GetAllAsync();
    //    return Ok(Slider);
    //}

    //[HttpPost]
    //public async Task<IActionResult> Post([FromForm] CarTypeCreateDTO carTypeCreateDTO)
    //{
    //    await _carService.CreateAsync(carTypeCreateDTO);
    //    return StatusCode((int)HttpStatusCode.Created);
    //}


    //[HttpGet("{id:Guid}")]
    //public async Task<IActionResult> GetById(Guid id)
    //{
    //    var BySlider = await _carService.GetByIdAsync(id);
    //    return Ok(BySlider);
    //}

    //[HttpGet("type")]
    //public async Task<IActionResult> GetByType(string type)
    //{
    //    var BySlider = await _carService.GetByNameAsync(type);
    //    return Ok(BySlider);
    //}

    //[HttpDelete("{id:Guid}")]
    //public async Task<IActionResult> Remove(Guid id)
    //{
    //    await _carService.RemoveAsync(id);
    //    return Ok();
    //}

    //[HttpPut("{id:Guid}")]
    //public async Task<IActionResult> Uptade(Guid id, [FromForm] CarTypeUpdateDTO carTypeUpdateDTO)
    //{
    //    await _carService.UpdateAsync(id, carTypeUpdateDTO);
    //    return Ok();
    //}

}
