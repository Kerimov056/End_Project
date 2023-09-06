using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Auth;
using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Helpers;
using EndProjet.Persistance.Context;
using Microsoft.AspNetCore.Authorization;
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

        public AuthController(IAuthService authService,
            IEmailService emailService,
            AppDbContext appDbContext,
            SignInManager<AppUser> signInManager)
        {
            _authService = authService;
            _emailService = emailService;
            _appDbContext = appDbContext;
            _siginManager = signInManager;
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

        [HttpPost("AdminCreate")]
        public async Task<IActionResult> AdminCreate([FromQuery] string superAdminId, [FromQuery] string appUserId)
        {
            await _authService.AdminCreate(superAdminId, appUserId);
            return StatusCode((int)HttpStatusCode.Created);
        }


        [HttpGet("AllMember")]
        public async Task<IActionResult> AllMemberUsers()
        {
            var memberUsers = await _authService.AllMemberUser();
            return Ok(memberUsers);
        }

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("account/external-auth-callback")]
        //public async Task<IActionResult> ExternalLoginCallback()
        //{
        //    ExternalLoginInfo info = await _siginManager.GetExternalLoginInfoAsync();
        //    var result = await _authService.ExternalLogin(info);

        //}








        //[HttpPost("Forget-Password")]
        //public async Task<IActionResult> ForgetPassword(string email)
        //{
        //    var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        //    if (user is null) return BadRequest("User not Found");

        //    user.RefreshTokenExpration
        //}

        // private string CreateRandomToken()
        // {
        //     return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        // }
    }
}
