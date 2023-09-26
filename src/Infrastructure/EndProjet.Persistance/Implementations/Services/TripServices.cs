using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Application.DTOs.Trip;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Authentication;

namespace EndProjet.Persistance.Implementations.Services;

public class TripServices : ITripServices
{
    private readonly ITripeReadRepository _tripReadRepository;
    private readonly ITripeWriteRepository _tripWriteRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;


    public TripServices(ITripeReadRepository tripReadRepository,
                        ITripeWriteRepository tripWriteRepository,
                        IMapper mapper,
                        UserManager<AppUser> userManager)
    {
        _tripReadRepository = tripReadRepository;
        _tripWriteRepository = tripWriteRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task CreateAsync(TripCreateDTO tripCreateDTO)
    {
        var appUser = await _userManager.FindByIdAsync(tripCreateDTO.AppUserId);
        if (appUser is null) throw new NotFoundException("Not Found User");

        //double lat = Convert.ToDouble(tripCreateDTO.TripLatitude, CultureInfo.InvariantCulture);
        //double lng = Convert.ToDouble(tripCreateDTO.TripLongitude, CultureInfo.InvariantCulture);


        var newTrip = _mapper.Map<Trip>(tripCreateDTO);
        //newTrip.TripLongitude = lat;
        //newTrip.TripLatitude = lng;
        await _tripWriteRepository.AddAsync(newTrip);
        await _tripWriteRepository.SavaChangeAsync();
    }

    public async Task<List<TripGetDTO>> GetAllAsync(string AppUserId)
    {
        var appUser = await _userManager.FindByIdAsync(AppUserId);
        if (appUser is null) throw new NotFoundException("Not Found User");

        var allTrips = await _tripReadRepository.GetAll()
                       .Where(x => x.AppUserId == AppUserId)
                       .OrderByDescending(x => x.CreatedDate)
                       .ToListAsync();

        var toDto = _mapper.Map<List<TripGetDTO>>(allTrips);    
        return toDto;
    }

    public async Task<TripGetDTO> GetByIdAsync(Guid Id)
    {
        var byTrip = await _tripReadRepository.GetByIdAsync(Id);
        if (byTrip is null) throw new NotFoundException("Trip not Found");

        var toDto = _mapper.Map<TripGetDTO>(byTrip);
        return toDto;
    }

    public async Task RemoveAsync(Guid tripId, string AppUserId)
    {
        var byTrip = await _tripReadRepository.GetByIdAsync(tripId);
        if (byTrip is null) throw new NotFoundException("Trip not Found");

        if (byTrip.AppUserId != AppUserId)
            throw new AuthenticationException("No Access");

        _tripWriteRepository.Remove(byTrip);
        await _tripWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, TripUpdateDTO tripUpdateDTO)
    {
        var byTrip = await _tripReadRepository.GetByIdAsync(id);
        if (byTrip is null) throw new NotFoundException("Trip not Found");

        if (byTrip.AppUserId != tripUpdateDTO.AppUserId)
            throw new AuthenticationException("No Access");

        _mapper.Map(tripUpdateDTO, byTrip);
        _tripWriteRepository.Update(byTrip);
        await _tripWriteRepository.SavaChangeAsync();
    }
}
