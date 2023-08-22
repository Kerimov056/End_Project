using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.CarImage;

public class CarImageGetDTO
{
    public Guid Id { get; set; }
    public Guid CarId { get; set; }
    public string ImagePath { get; set; }
}
