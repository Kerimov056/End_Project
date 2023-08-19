using EndProject.Application.DTOs.Chauffeurs;
using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface IChauffeursServices
{
    Task<List<ChauffeursGetDTO>> GetAllAsync();
    Task<List<ChauffeursGetDTO>> ViewGetAll();
    Task CreateAsync(ChauffeursCreateDTO chauffeursCreateDTO);
    Task<ChauffeursGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, ChauffeursUpdateDTO chauffeursUpdateDTO);
    Task IsChauffeurs(Guid? cheuffeursId);
    Task RemoveAsync(Guid id);
}
