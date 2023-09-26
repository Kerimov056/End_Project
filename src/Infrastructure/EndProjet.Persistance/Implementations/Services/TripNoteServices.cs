using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.TripNote;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using EndProject.Domain.Enums.Role;
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
    private readonly IShareTripServices _shareTripServices;
    private readonly IShareTripReadRepository _shareTripReadRepository;

    public TripNoteServices(ITripNoteReadRepository tripNoteReadRepository,
                            ITripNoteWriteRepository tripNoteWriteRepository,
                            IMapper mapper,
                            UserManager<AppUser> userManager,
                            ITripeReadRepository tripeReadRepository,
                            IShareTripServices shareTripServices,
                            IShareTripReadRepository shareTripReadRepository)
    {
        _tripNoteReadRepository = tripNoteReadRepository;
        _tripNoteWriteRepository = tripNoteWriteRepository;
        _mapper = mapper;
        _userManager = userManager;
        _tripeReadRepository = tripeReadRepository;
        _shareTripServices = shareTripServices;
        _shareTripReadRepository = shareTripReadRepository;
    }

    public async Task CreateAsync(TripNoteCreateDTO tripNoteCreateDTO)
    {
        var byTrip = await _tripeReadRepository
           .GetByIdAsyncExpression(x => x.Id == tripNoteCreateDTO.TripId);
        if (tripNoteCreateDTO.AppUserId != byTrip.AppUserId)
        {
            var byShareUser = await _userManager.FindByIdAsync(tripNoteCreateDTO.AppUserId);
            if (byShareUser is null) throw new NotFoundException("User not Found");

            var byContributor = await _shareTripReadRepository
                .GetByIdAsyncExpression(x => x.Email == byShareUser.Email &&
                                        x.TripRole == TripRole.Contributor);

            if (byContributor is null) throw new Exception("No Access");
        }
        
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

    public async Task<List<TripNoteGetDTO>> GetAllMyNote(string AppUserId, Guid TripId)
    {

        var allTripNote = await _tripNoteReadRepository
                                .GetAll()
                                .Include(x => x.Trip)
                                .Include(x => x.AppUser)
                                .Where(x => x.AppUserId == AppUserId)
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

    public async Task RemoveRangeAsync(Guid tripId)
    {
        var allTripNotes = await _tripNoteReadRepository
                         .GetAll()
                         .Where(x => x.TripId == tripId)
                         .ToListAsync();
        if (allTripNotes is null) return;

        _tripNoteWriteRepository.RemoveRange(allTripNotes);
        await _tripNoteWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, TripNoteUpdateDTO tripNoteUpdateDTO)
    {
        var TripNote = await _tripNoteReadRepository.GetByIdAsync(id);
        if (TripNote is null) throw new NotFoundException("TripNote is null");

        if (TripNote.AppUserId != tripNoteUpdateDTO.AppUserId) throw new Exception("No Acces");
        tripNoteUpdateDTO.CreateTripNote = DateTime.Now;

        _mapper.Map(tripNoteUpdateDTO, TripNote);
        TripNote.CreateTripNote = DateTime.Now;
        _tripNoteWriteRepository.Update(TripNote);
        await _tripNoteWriteRepository.SavaChangeAsync();
    }


}
