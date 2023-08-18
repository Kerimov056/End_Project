using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarType;

namespace EndProject.Application.Abstraction.Services;

public interface ICarServices
{
    Task<List<CarGetDTO>> GetAllAsync();
    Task CreateAsync(CarCreateDTO carCreateDTO);
    Task<CarGetDTO> GetByIdAsync(Guid Id);
    Task<List<CarGetDTO>> GetByNameAsync(string car);
    Task UpdateAsync(Guid id, CarUpdateDTO carUpdateDTO);
    Task RemoveAsync(Guid id);
}
