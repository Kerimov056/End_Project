using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Auth;
using EndProject.Application.DTOs.Auth.GoogleLogin;
using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Helpers;
using EndProjet.Persistance.Context;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EndProject.API.Controllersş
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<AppUser> _siginManager;
        private readonly IEmailService _emailService;
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;

        public AuthController(IAuthService authService,
            IEmailService emailService,
            AppDbContext appDbContext,
            SignInManager<AppUser> signInManager,
            IMediator mediator)
        {
            _authService = authService;
            _emailService = emailService;
            _appDbContext = appDbContext;
            _siginManager = signInManager;
            _mediator = mediator;

        }

        [HttpPost("register")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            ArgumentNullException.ThrowIfNull(registerDTO, ExceptionResponseMessages.ParametrNotFoundMessage);

            SignUpResponse response = await _authService.Register(registerDTO)
                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

            if (response.Errors != null)
            {
                if (response.Errors.Count > 0)
                {
                    return BadRequest(response.Errors);
                }
            }

            string subject = "Register Confirmation";
            string html = string.Empty;
            string password = registerDTO.password;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "verify.html");
            html = System.IO.File.ReadAllText(filePath);

            html = html.Replace("{{password}}", password);

            _emailService.Send(registerDTO.Email, subject, html);

            return Ok(response);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var responseToken = await _authService.Login(loginDTO);
            return Ok(responseToken);
        }

        [HttpPost("AdminLogin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginDTO loginDTO)
        {
            var responseToken = await _authService.LoginAdmin(loginDTO);
            return Ok(responseToken);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromQuery] string ReRefreshtoken)
        {
            var response = await _authService.ValidRefleshToken(ReRefreshtoken);
            return Ok(response);
        }


        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }



        [HttpPost("AdminCreate")]
        public async Task<IActionResult> AdminCreate([FromQuery] string superAdminId, [FromQuery] string appUserId)
        {
            await _authService.AdminCreate(superAdminId, appUserId);
            return StatusCode((int)HttpStatusCode.Created);
        }
        
        [HttpPost("AdminDelete")]
        public async Task<IActionResult> AdminDelete([FromQuery] string superAdminId, [FromQuery] string appUserId)
        {
            await _authService.AdminDelete(superAdminId, appUserId);
            return Ok();
        }

        [HttpGet("AllAdmin")]
        public async Task<IActionResult> AllAdminUsers([FromQuery] string? searchUser)
        {
            var adminUsers = await _authService.AllAdminUser(searchUser);
            return Ok(adminUsers);
        }


        [HttpGet("AllMember")]
        public async Task<IActionResult> AllMemberUsers([FromQuery] string? searchUser)
        {
            var memberUsers = await _authService.AllMemberUser(searchUser);
            return Ok(memberUsers);
        }
        [HttpGet("ByUser")]
        public async Task<IActionResult> ByUser([FromQuery] string? userId)
        {
            var Users = await _authService.ByUser(userId);
            return Ok(Users);
        }


        [HttpDelete("RemoveUser")]
        public async Task<IActionResult> UserRemove([FromQuery] string superAdminId, [FromQuery] string userId)
        {
            await _authService.RemoveUser(superAdminId, userId);
            return Ok();
        }


    }
}
