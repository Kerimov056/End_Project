using EndProject.Application.DTOs.CarType;
using EndProject.Application.DTOs.Category;
using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.Car;

public class CarCreateDTO
{
    public string Marka { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string Description { get; set; }
    public CarTypeCreateEDTO CarType { get; set; }
    public CarCategoryGetTDTO CarCategory { get; set; }
    public List<IFormFile> CarImages { get; set; }
    public List<string> tags { get; set; }
}

