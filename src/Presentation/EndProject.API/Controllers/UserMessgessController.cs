using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserMessgessController : ControllerBase
{
    private readonly ISendUserMessageServices _sendUserMessageServices;
    private readonly IEmailService _emailService;

    public UserMessgessController(ISendUserMessageServices sendUserMessageServices, IEmailService emailService)
    {
        _sendUserMessageServices = sendUserMessageServices;
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> ByUserEmailMessage([FromBody] UserEmailMessageDTO userEmailMessage)
    {
        await _sendUserMessageServices.ByUserEmailMessage(userEmailMessage);


        //string subject = "LuxeDrive Message";
        //string html = string.Empty;

        //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "UserEmailMessage.html");
        //html = System.IO.File.ReadAllText(filePath);

        //html = html.Replace("{{Message}}", userEmailMessage.Message);

        //_emailService.Send(userEmailMessage.Email, subject, html);

        return Ok();
    }
}
