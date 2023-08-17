using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.CarImage;

public class CarImageGetDTO
{
    public Guid Id { get; set; }
    public IFormFile iamge { get; set; }
}
