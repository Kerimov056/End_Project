using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarType;

namespace EndProject.Application.Abstraction.Services;

public interface ICarServices
{
    Task<List<CarGetDTO>> GetAllAsync();
    Task CreateAsync(CarCreateDTO carCreateDTO);
    Task<CarGetDTO> GetByIdAsync(Guid Id);
    Task<List<CarGetDTO>> GetByNameAsync(string? car, string? model);
    Task<List<CarGetDTO>> GetSearchCar(string? category, string? type, string? marka, string? model, decimal? minPrice, decimal? maxPrice);
    Task UpdateAsync(Guid id, CarUpdateDTO carUpdateDTO);
    Task ReservCarTrue(Guid Id);
    Task ReservCarFalse(Guid Id);
    Task RemoveAsync(Guid id);
    Task<int> GetCarCountAsync();
    Task<int> GetReservCarCountAsync();
}
