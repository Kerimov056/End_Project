using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Trip;
using EndProject.Application.DTOs.TripNote;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class TripNoteServices : ITripNoteServices
{
    private readonly ITripNoteReadRepository _tripNoteReadRepository;
    private readonly ITripNoteWriteRepository _tripNoteWriteRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITripeReadRepository _tripeReadRepository;

    public TripNoteServices(ITripNoteReadRepository tripNoteReadRepository,
                            ITripNoteWriteRepository tripNoteWriteRepository,
                            IMapper mapper,
                            UserManager<AppUser> userManager,
                            ITripeReadRepository tripeReadRepository)
    {
        _tripNoteReadRepository = tripNoteReadRepository;
        _tripNoteWriteRepository = tripNoteWriteRepository;
        _mapper = mapper;
        _userManager = userManager;
        _tripeReadRepository = tripeReadRepository;
    }

    public async Task CreateAsync(TripNoteCreateDTO tripNoteCreateDTO)
    {
        var appUser = await _userManager.FindByIdAsync(tripNoteCreateDTO.AppUserId);
        if (appUser is null) throw new NotFoundException("Not Found User");

        var newTripNote = _mapper.Map<TripNote>(tripNoteCreateDTO);

        await _tripNoteWriteRepository.AddAsync(newTripNote);
        await _tripNoteWriteRepository.SavaChangeAsync();
    }

    public async Task<List<TripNoteGetDTO>> GetAllAsync(Guid TripId)
    {
        var byTrip = await _tripeReadRepository.GetByIdAsync(TripId);
        if (byTrip is null) throw new NotFoundException("Trip is null");

        var allTripNote = await _tripNoteReadRepository
                                .GetAll()
                                .Include(x => x.Trip)
                                .Include(x => x.AppUser)
                                .Where(x => x.TripId == TripId)
                                .ToListAsync();

        var toDto = _mapper.Map<List<TripNoteGetDTO>>(allTripNote);
        return toDto;
    }

    public async Task<TripNoteGetDTO> GetByIdAsync(Guid Id)
    {
        var TripNote = await _tripNoteReadRepository.GetByIdAsync(Id);
        if (TripNote is null) throw new NotFoundException("TripNote is null");

        var toDto = _mapper.Map<TripNoteGetDTO>(TripNote);
        return toDto;
    }

    public async Task RemoveAsync(Guid id, string AppUserId)
    {
        var TripNote = await _tripNoteReadRepository.GetByIdAsync(id);
        if (TripNote is null) throw new NotFoundException("TripNote is null");

        if (TripNote.AppUserId != AppUserId) throw new Exception("No Acces");

        _tripNoteWriteRepository.Remove(TripNote);
        await _tripNoteWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, TripNoteUpdateDTO tripNoteUpdateDTO)
    {
        var TripNote = await _tripNoteReadRepository.GetByIdAsync(id);
        if (TripNote is null) throw new NotFoundException("TripNote is null");

        _mapper.Map(tripNoteUpdateDTO, TripNote);
        _tripNoteWriteRepository.Update(TripNote);
        await _tripNoteWriteRepository.SavaChangeAsync();
    }
}
