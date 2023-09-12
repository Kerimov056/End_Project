using EndProject.Application.Abstraction.Services;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Context;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class CheckoutController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ICarServices _carServices;
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;
    public CheckoutController(AppDbContext context, ICarServices carServices)
    {
        _context = context;
        _carServices = carServices;
    }

    


}