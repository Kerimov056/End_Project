using EndProject.Application.DTOs.CarImage;
using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface ICarImageServices
{
    Task<List<CarImageGetDTO>> GetAllAsync();
    Task CreateAsync(CarImageCreateDTO carImageCreateDTO);
    Task<CarImageGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, CarImageUpdateDTO carImageUpdateDTO);
    Task RemoveAsync(Guid id);
}
