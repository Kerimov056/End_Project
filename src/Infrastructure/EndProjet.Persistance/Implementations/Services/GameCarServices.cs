using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services.Game;
using EndProject.Application.DTOs.Game;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class GameCarServices : IGameCarServices
{
    private readonly IGameCarReadRepository _gameCarReadRepository;
    private readonly IGameCarWriteRepository _gameCarWriteRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly ICarReadRepository _carReadRepository;
    private readonly IMapper _mapper;

    public GameCarServices(IGameCarReadRepository gameCarReadRepository,
                           IGameCarWriteRepository gameCarWriteRepository,
                           IMapper mapper,
                           UserManager<AppUser> userManager,
                           ICarReadRepository carReadRepository)
    {
        _gameCarReadRepository = gameCarReadRepository;
        _gameCarWriteRepository = gameCarWriteRepository;
        _mapper = mapper;
        _userManager = userManager;
        _carReadRepository = carReadRepository;
    }

    public async Task CreateAsync(GameCarCreateDTO gameCarCreateDTO)
    {
        var appUser = await _userManager.FindByIdAsync(gameCarCreateDTO.AppUserId);
        if (appUser is null) throw new NotFoundException("Not Found User");

        var newGameProfil = _mapper.Map<GameCar>(gameCarCreateDTO);
        newGameProfil.Win = true;
        await _gameCarWriteRepository.AddAsync(newGameProfil);
        await _gameCarWriteRepository.SavaChangeAsync();
    }

    public async Task<bool> GameResponse(string AppUserId)
    {
        var appUser = await _userManager.FindByIdAsync(AppUserId);
        if (appUser is null) throw new NotFoundException("Not Found User");

        var byUserGame = await _gameCarReadRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.AppUserId == AppUserId);

        if (byUserGame is null) return false;

        Guid SPassword = byUserGame.CarId;
        string fakePassword = SPassword.ToString().Replace("-", "");
        if (fakePassword == byUserGame.Password) return true;
        return false;
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

    public async Task<GameCarGetDTO> GetByIdAsync(string AppUserId)
    {
        var byGameCarProfile = await _gameCarReadRepository.GetByIdAsyncExpression(x => x.AppUserId == AppUserId);
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
