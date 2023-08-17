using EndProject.Application.DTOs.Category;

namespace EndProject.Application.Abstraction.Services;

public interface ICarCategoryServices
{
    Task<List<CarCategoryGetDTO>> GetAllAsync();
    Task CreateAsync(CarCategoryCreateDTO carCategoryCreateDTO);
    Task<CarCategoryGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, CarCategoryUpdateDTO carCategoryUpdateDTO);
    Task RemoveAsync(Guid id);
}
