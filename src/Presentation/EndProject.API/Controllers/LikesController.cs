﻿using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Like;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{
    private readonly ILikeServices _likeServices;

    public LikesController(ILikeServices likeServices)
    =>   _likeServices = likeServices;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Slider = await _likeServices.GetAllAsync();
        return Ok(Slider);
    }

    [HttpPost]
    public async Task<IActionResult> Post([Required][FromQuery] string AppUserId, [Required][FromQuery] Guid CarCommentId)
    {
        await _likeServices.CreateAsync(AppUserId, CarCommentId);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var BySlider = await _likeServices.GetByIdAsync(id);
        return Ok(BySlider);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        await _likeServices.RemoveAsync(id);
        return Ok();
    }

}
