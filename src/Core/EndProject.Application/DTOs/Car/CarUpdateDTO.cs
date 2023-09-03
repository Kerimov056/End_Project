using EndProject.Application.DTOs.CarType;
using EndProject.Application.DTOs.Category;
using EndProject.Domain.Entitys;
using Microsoft.AspNetCore.Http;

namespace EndProject.Application.DTOs.Car;

public class CarUpdateDTO
{
    public string Marka { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public bool isReserv { get; set; } = false;
    public CarTypeUpdateDTO CarType { get; set; }
    public CarCategoryUpdateDTO CarCategory { get; set; }
    public List<IFormFile> CarImages { get; set; }
    public List<string> tags { get; set; }
}
