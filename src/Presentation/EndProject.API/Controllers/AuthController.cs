﻿using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllersş
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            await _authService.Register(registerDTO);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var responseToken = await _authService.Login(loginDTO);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromQuery] string ReRefreshtoken)
        {
            var response = await _authService.ValidRefleshToken(ReRefreshtoken);
            return Ok(response);
        }
    }
}