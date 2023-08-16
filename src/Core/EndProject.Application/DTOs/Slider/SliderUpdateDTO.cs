using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.Slider;

public class SliderUpdateDTO
{
    public IFormFile image { get; set; }
}
