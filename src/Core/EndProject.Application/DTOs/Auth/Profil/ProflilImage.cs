using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.Auth.Profil;

public class ProflilImage
{
    public string? Email { get; set; }
    public IFormFile? ImageFile { get; set; }
}
