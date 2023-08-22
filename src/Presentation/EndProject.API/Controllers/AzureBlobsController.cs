using EndProject.Infrastructure.Services.Azure;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AzureBlobsController : ControllerBase
{
    public AzureBlobService _azureBlobService;

    public AzureBlobsController(AzureBlobService azureBlobService)
        =>   _azureBlobService = azureBlobService;

    [HttpPost]
    public async Task<IActionResult> UploadFiles(List<IFormFile> files)
    {
        var response = await _azureBlobService.UploadFiles(files);
        return Ok(response);    
    }


    [HttpGet]
    public async Task<IActionResult> GetBlobList()
    {
        var response = await _azureBlobService.GetUploadedBlob();
        return Ok(response);
    }
}
