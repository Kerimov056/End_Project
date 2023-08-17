using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarType;
using EndProject.Application.DTOs.Category;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarCategoryController : ControllerBase
{
    private readonly ICarCategoryServices _carCategoryServices;

    public CarCategoryController(ICarCategoryServices carCategoryServices) => _carCategoryServices = carCategoryServices;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _carCategoryServices.GetAllAsync();
        return Ok(Slider);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CarCategoryCreateDTO carCategoryCreateDTO)
    {
        await _carCategoryServices.CreateAsync(carCategoryCreateDTO);
        return StatusCode((int)HttpStatusCode.Created);
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _carCategoryServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _carCategoryServices.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] CarCategoryUpdateDTO carCategoryUpdateDTO)
    {
        await _carCategoryServices.UpdateAsync(id, carCategoryUpdateDTO);
        return Ok();
    }
}
