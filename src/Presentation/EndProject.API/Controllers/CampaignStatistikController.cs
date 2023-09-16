using EndProject.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CampaignStatistikController : ControllerBase
{
    private readonly ICampaignStatistikaServices _campaignStatistikaServices;

    public CampaignStatistikController(ICampaignStatistikaServices campaignStatistikaServices)
    =>  _campaignStatistikaServices = campaignStatistikaServices;


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Statistika = await _campaignStatistikaServices.GetAllAsync();
        return Ok(Statistika);
    }
}
