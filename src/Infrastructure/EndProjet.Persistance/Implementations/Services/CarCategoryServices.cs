using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Category;

namespace EndProjet.Persistance.Implementations.Services;

public class CarCategoryServices : ICarCategoryServices
{
    public Task CreateAsync(CarCategoryCreateDTO carCategoryCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<CarCategoryGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarCategoryGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, CarCategoryUpdateDTO carCategoryUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
