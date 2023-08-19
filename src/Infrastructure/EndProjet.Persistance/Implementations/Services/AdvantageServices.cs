using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Advantage;

namespace EndProjet.Persistance.Implementations.Services;

public class AdvantageServices : IAdvantageServices
{
    public Task CreateAsync(AdvantageCreateDTO advantageCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<AdvantageGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AdvantageGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, AdvantageUpdateDTO advantageUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
