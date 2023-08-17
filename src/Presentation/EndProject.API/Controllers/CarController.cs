using AutoMapper;
using EndProject.Application.DTOs.Car;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Context;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _appDbContext;

    public CarController(IMapper mapper, AppDbContext appDbContext)
    {
        _mapper = mapper;
        _appDbContext = appDbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCar(CarCreateDTO carCreateDTO)
    {
        var newCar = _mapper.Map<Car>(carCreateDTO);
        await _appDbContext.Cars.AddAsync(newCar);
        await _appDbContext.SaveChangesAsync();
        return Ok(newCar);
    }
}
