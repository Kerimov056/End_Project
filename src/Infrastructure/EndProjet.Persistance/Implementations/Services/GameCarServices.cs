using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services.Game;
using EndProject.Application.DTOs.Game;
using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

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

    public async Task CreateAsync(GameCarCreateDTO gameCarCreateDTO)
    {
        gameCarCreateDTO.Win = true;
        var newGameProfil = _mapper.Map<GameCar>(gameCarCreateDTO);
        newGameProfil.Win = true;
        await _gameCarWriteRepository.AddAsync(newGameProfil);
        await _gameCarWriteRepository.SavaChangeAsync();
    }

    public async Task<List<GameCarGetDTO>> GetAllAsync()
    {
        var allGameCarProfile = await _gameCarReadRepository.GetAll().ToListAsync();
        var toDto = _mapper.Map<List<GameCarGetDTO>>(allGameCarProfile);
        return toDto;
    }

    public async Task<GameCarGetDTO> GetByIdAsync(Guid Id)
    {
        var byGameCarProfile = await _gameCarReadRepository.GetByIdAsync(Id);
        if (byGameCarProfile is null) throw new DirectoryNotFoundException();

        var toDto = _mapper.Map<GameCarGetDTO>(byGameCarProfile);
        return toDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var byGameCarProfile = await _gameCarReadRepository.GetByIdAsync(id);
        if (byGameCarProfile is null) throw new DirectoryNotFoundException();

        _gameCarWriteRepository.Remove(byGameCarProfile);
        await _gameCarWriteRepository.SavaChangeAsync();
    }
}
