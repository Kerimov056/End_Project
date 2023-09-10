using EndProject.Application.DTOs.CarImage;

namespace EndProject.Application.Abstraction.Services;

public interface ICarImageServices
{
    Task<List<CarImageGetDTO>> GetAllAsync();
    Task<List<CarImageGetDTO>> GetAllCarIdAsync(Guid carId);
    Task CreateAsync(CarImageCreateDTO carImageCreateDTO);
    Task<CarImageGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, CarImageUpdateDTO carImageUpdateDTO);
    Task RemoveAsync(Guid id);
}
