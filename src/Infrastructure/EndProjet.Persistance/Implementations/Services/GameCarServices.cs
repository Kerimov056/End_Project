using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services.Game;
using EndProject.Application.DTOs.Game;

namespace EndProjet.Persistance.Implementations.Services;

public class GameCarServices : IGameCarServices
{
    private readonly IGameCarReadRepository _gameCarReadRepository;
    private readonly IGameCarWriteRepository _gameCarWriteRepository;
    private readonly IMapper _mapper;

    public GameCarServices(IGameCarReadRepository gameCarReadRepository,
                           IGameCarWriteRepository gameCarWriteRepository,
                           IMapper mapper)
    {
        _gameCarReadRepository = gameCarReadRepository;
        _gameCarWriteRepository = gameCarWriteRepository;
        _mapper = mapper;
    }

    public Task CreateAsync(GameCarCreateDTO gameCarCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<GameCarGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GameCarGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
