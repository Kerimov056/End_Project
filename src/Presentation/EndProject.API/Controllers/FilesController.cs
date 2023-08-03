using EndProject.Application.Abstraction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IStorgeService _service;

    
}
