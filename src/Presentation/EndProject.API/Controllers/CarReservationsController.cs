﻿using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Domain.Entitys.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarReservationsController : ControllerBase
{
    private readonly ICarReservationServices _carReservationServices;
    private readonly IEmailService _emailService;
    private readonly ICarServices _carServices;

    public CarReservationsController(ICarReservationServices carReservationServices,
                                     IEmailService emailService,
                                     ICarServices carServices)
    {
        _carReservationServices = carReservationServices;
        _emailService = emailService;
        _carServices = carServices;
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

    [HttpGet("ReservNowCount")]
    public async Task<IActionResult> GetReservNow()
    {
        var reserv = await _carReservationServices.GetCanceledNowAsync();
        return Ok(reserv);
    }

    [HttpGet("IsResevConfirmedGetAll")]
    public async Task<IActionResult> ReservGetAllConfirmed()
    {
        var Slider = await _carReservationServices.IsResevConfirmedGetAll();
        return Ok(Slider);
    }

    [HttpGet("IsResevConfirmedLocationGetAll")]
    public async Task<IActionResult> ReservGetAllConfirmedLocatıon()
    {
        var Slider = await _carReservationServices.IsResevConfirmLocationGetAll();
        return Ok(Slider);
    }
    [HttpGet("IsResevConfirmedPickUpGetAll")]
    public async Task<IActionResult> ReservGetAllConfirmPickUpLocation()
    {
        var Slider = await _carReservationServices.IsResevConfirmPickUpGetAll();
        return Ok(Slider);
    }

    [HttpGet("IsResevConfirmedReturnGetAll")]
    public async Task<IActionResult> ReservGetAllConfirmReturnLocation()
    {
        var Slider = await _carReservationServices.IsResevConfirmReturnGetAll();
        return Ok(Slider);
    }

    [HttpGet("NotCompaignStaitsika")]
    public async Task<IActionResult> GetReservStatistik()
    {
        var statistik = await _carReservationServices.NotCompaignStaitsika();
        return Ok(statistik);
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
        //string subject = "There is a new reservation";
        //string html = string.Empty;

        //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "newReservation.html");
        //html = System.IO.File.ReadAllText(filePath);

        //var byCar = await _carServices.GetByIdAsync(carReservationCreateDTO.CarId);
        //var QrImage = $"https://localhost:7152/api/Car/qrcodeImage?id={carReservationCreateDTO.CarId}";

        //html = html.Replace("{{Marka}}", byCar.Marka);
        //html = html.Replace("{{Model}}", byCar.Model);
        //html = html.Replace("{{QrCodeCar}}", QrImage);   

        //_emailService.Send(carReservationCreateDTO.Email, subject, html);

        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPost("AllResev")]
    public async Task<IActionResult> AllPost([FromForm] AllCarReservation AllCarReservation)
    {
        await _carReservationServices.AllCreateAsync(AllCarReservation);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _carReservationServices.GetByIdAsync(id);
        return Ok(BySlider);
    }


    [HttpGet("CarReservValue")]
    public async Task<IActionResult> GetCarReservValue(Guid CarId)
    {
        var ByReserv = await _carReservationServices.GetReservValue(CarId);
        return Ok(ByReserv);
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


    //Game

    [HttpGet("CarFindGameUserAccess")]
    public async Task<IActionResult> CarFindGameAccess(string AppUserId)
    {
        var response = await _carReservationServices.CarFindGameUserAccess(AppUserId);
        return Ok(response);
    }


    //Confirem Reservation PickUp and Return Date 

    [HttpGet("ConformPickUpDate")]
    public async Task<IActionResult> GetAllConformPickDate(Guid CarId)
    {
        var result = await _carReservationServices.ConformPickUpDate(CarId);
        return Ok(result);
    }   
    
    [HttpGet("ConformReturnDate")]
    public async Task<IActionResult> GetAllConformReturnDate(Guid CarId)
    {
        var result = await _carReservationServices.ConformReturnDate(CarId);
        return Ok(result);
    }
}
