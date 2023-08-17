using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.CarImage;

public class CarImageCreateDTO
{
    public IFormFile image { get; set; }
    public Guid CarId { get; set; }
}
