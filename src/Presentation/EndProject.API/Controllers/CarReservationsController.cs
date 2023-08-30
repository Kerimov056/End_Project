using Azure;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Enums.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarReservationsController : ControllerBase
{
    private readonly ICarReservationServices _carReservationServices;
    private readonly IEmailService _emailService;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public CarReservationsController(ICarReservationServices carReservationServices,
                                     IEmailService emailService,
                                     UserManager<AppUser> userManager,
                                     RoleManager<IdentityRole> roleManager)
    {
        _carReservationServices = carReservationServices;
        _emailService = emailService;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _carReservationServices.GetAllAsync();
        return Ok(Slider);
    }

    [HttpGet("resercPeddingCount")]
    public async Task<IActionResult> GetReservPeddingCount()
    {
        var peddingCount = await _carReservationServices.GetPeddingCountAsync();
        return Ok(peddingCount);
    }

    [HttpGet("resercPedding")]
    public async Task<IActionResult> GetReservPedding()
    {
        var Slider = await _carReservationServices.IsResevPedingGetAll();
        return Ok(Slider);
    }

    [HttpGet("ReservConfirmedCount")]
    public async Task<IActionResult> GetReservConfirmed()
    {
        var reserv = await _carReservationServices.GetConfirmedCountAsync();
        return Ok(reserv);
    }
    [HttpGet("IsResevConfirmedGetAll")]
    public async Task<IActionResult> ReservGetAllConfirmed()
    {
        var Slider = await _carReservationServices.IsResevConfirmedGetAll();
        return Ok(Slider);
    }

    [HttpGet("ReservComplatedCount")]
    public async Task<IActionResult> GetReservComplated()
    {
        var reserv = await _carReservationServices.GetCompletedCountAsync();
        return Ok(reserv);
    }


    [HttpGet("IsResevComplatedGetAll")]
    public async Task<IActionResult> ReservGetAllComplated()
    {
        var Slider = await _carReservationServices.IsResevComplatedGetAll();
        return Ok(Slider);
    }

    [HttpGet("IsResevCanceledCount")]
    public async Task<IActionResult> ReservGetAllCanceledCount()
    {
        var reservCount = await _carReservationServices.GetCanceledCountAsync();
        return Ok(reservCount);
    }

    [HttpGet("IsResevCanceledGetAll")]
    public async Task<IActionResult> ReservGetAllCanceled()
    {
        var Slider = await _carReservationServices.IsResevCanceledGetAll();
        return Ok(Slider);
    }

    [HttpGet("IsResevNowGetAll")]
    public async Task<IActionResult> ReservGetAllNow()
    {
        var Slider = await _carReservationServices.IsResevNowGetAll();
        return Ok(Slider);
    }


    [HttpGet("UserId")]
    public async Task<IActionResult> YouGetAll(string Id)
    {
        var Slider = await _carReservationServices.YouGetAllAsync(Id);
        return Ok(Slider);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CarReservationCreateDTO carReservationCreateDTO)
    {
        await _carReservationServices.CreateAsync(carReservationCreateDTO);
        string subject = "There is a new reservation";
        string html = string.Empty;

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "Cancel.html");
        html = System.IO.File.ReadAllText(filePath);

        var adminRoles = await _roleManager.Roles
         .Where(r => r.Name == "Admin" || r.Name == "SuperAdmin")
         .ToListAsync();

        var adminUsersList = new List<AppUser>();

        foreach (var role in adminRoles)
        {
            var adminUsers = await _userManager.GetUsersInRoleAsync(role.Name);
            adminUsersList.AddRange(adminUsers);
        }

        var adminUserEmails = adminUsersList.Select(user => user.Email).ToList();
        foreach (var item in adminUserEmails)
        {
            _emailService.Send(item, subject, html);
        }

        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPost("AllResev")]
    public async Task<IActionResult> AllPost([FromForm] ReservationFake ReservationFake)
    {
        await _carReservationServices.AllCreateAsync(ReservationFake);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _carReservationServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _carReservationServices.RemoveAsync(id);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Uptade(Guid id, [FromForm] CarReservationUpdateDTO carReservationUpdateDTO)
    {
        await _carReservationServices.UpdateAsync(id, carReservationUpdateDTO);
        return Ok();
    }

    [HttpPut("Confirmed")]
    public async Task<IActionResult> UptadeStatus(Guid Id)
    {
        await _carReservationServices.StatusConfirmed(Id);
        var byReserv = await _carReservationServices.GetByIdAsync(Id);
        string subject = "Confirmation message";
        string html = string.Empty;

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "confirm.html");
        html = System.IO.File.ReadAllText(filePath);

        _emailService.Send(byReserv.Email, subject, html);

        return Ok();
    }

    [HttpPut("Cancled")]
    public async Task<IActionResult> UptadeStatusCancled(Guid Id)
    {
        await _carReservationServices.StatusCanceled(Id);
        var byReserv = await _carReservationServices.GetByIdAsync(Id);
        string subject = "Was not accepted message";
        string html = string.Empty;

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "Cancel.html");
        html = System.IO.File.ReadAllText(filePath);

        _emailService.Send(byReserv.Email, subject, html);
        return Ok();
    }

    [HttpPut("Complated")]
    public async Task<IActionResult> UptadeStatusComplated(Guid Id)
    {
        await _carReservationServices.StatusCompleted(Id);
        return Ok();
    }

    [HttpPut("Now")]
    public async Task<IActionResult> UptadeStatusNow(Guid Id)
    {
        await _carReservationServices.StatusNow(Id);
        return Ok();
    }
}
