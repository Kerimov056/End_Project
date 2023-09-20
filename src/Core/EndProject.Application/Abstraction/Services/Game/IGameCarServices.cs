using EndProject.Application.DTOs.Game;

namespace EndProject.Application.Abstraction.Services.Game;

public interface IGameCarServices
{
    Task<List<GameCarGetDTO>> GetAllAsync();
    Task CreateAsync(GameCarCreateDTO gameCarCreateDTO);
    Task<GameCarGetDTO> GetByIdAsync(Guid Id);
    Task<bool> GameResponse(string AppUserId);
    Task RemoveAsync(Guid id);
}
