using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.Slider;

public class SliderCreateDTO
{
    public IFormFile image { get; set; }
}
