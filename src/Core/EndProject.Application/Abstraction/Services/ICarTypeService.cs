using EndProject.Application.DTOs.CarType;

namespace EndProject.Application.Abstraction.Services;

public interface ICarTypeService
{
    Task<List<CarTypeGetDTO>> GetAllAsync();
    Task CreateAsync(CarTypeCreateDTO carTypeCreateDTO);
    Task<CarTypeGetDTO> GetByIdAsync(Guid Id);
    Task<CarTypeGetDTO> GetByNameAsync(string type);
    Task UpdateAsync(Guid id, CarTypeUpdateDTO carTypeUpdateDTO);
    Task RemoveAsync(Guid id);
}
