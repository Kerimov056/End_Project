using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserMessgessController : ControllerBase
{
    private readonly ISendUserMessageServices _sendUserMessageServices;

    public UserMessgessController(ISendUserMessageServices sendUserMessageServices)
     =>   _sendUserMessageServices = sendUserMessageServices;


    [HttpPost]
    public async Task<IActionResult> ByUserEmailMessage([FromBody] UserEmailMessageDTO userEmailMessage)
    {
        await _sendUserMessageServices.ByUserEmailMessage(userEmailMessage);
        return Ok();
    }
}
