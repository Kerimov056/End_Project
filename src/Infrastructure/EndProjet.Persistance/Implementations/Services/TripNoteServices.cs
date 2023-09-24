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
    //private readonly TripServices _tripServices;
    private readonly ITripeReadRepository _tripeReadRepository;

    public TripNoteServices(ITripNoteReadRepository tripNoteReadRepository,
                            ITripNoteWriteRepository tripNoteWriteRepository,
                            IMapper mapper,
                            UserManager<AppUser> userManager,
                            ITripeReadRepository tripeReadRepository
        //TripServices tripServices
        )
    {
        _tripNoteReadRepository = tripNoteReadRepository;
        _tripNoteWriteRepository = tripNoteWriteRepository;
        _mapper = mapper;
        _userManager = userManager;
        _tripeReadRepository = tripeReadRepository;
        //_tripServices = tripServices;
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
        //var byTrip = await _tripServices.GetByIdAsync(TripId);
        //if (byTrip is null) throw new NotFoundException("Trip is null");

        //var allTripNote = await _tripNoteReadRepository
        //                    .GetAll()
        //                    .Include(x => x.Trip)
        //                    .Include(x => x.AppUser)
        //                    .Where(x => x.TripId == TripId)
        //                    .OrderByDescending(x => x.CreatedDate)
        //                    .ToListAsync();

        //var toDto = _mapper.Map<List<TripNoteGetDTO>>(allTripNote); 
        //return toDto;
        throw new NotImplementedException();

    }

    public Task<TripNoteGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, TripNoteUpdateDTO tripNoteUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
