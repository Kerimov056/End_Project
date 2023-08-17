using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarType;
using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface ICarTypeService
{
    Task<List<CarTypeGetDTO>> GetAllAsync();
    Task CreateAsync(CarTypeCreateDTO carTypeCreateDTO);
    Task<CarGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, CarTypeUpdateDTO carTypeUpdateDTO);
    Task RemoveAsync(Guid id);
}
