using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Domain.Entitys;
using EndProject.Domain.Enums.ReservationStatus;
using EndProjet.Persistance.Exceptions;

namespace EndProjet.Persistance.Implementations.Services;

public class CarReservationServices : ICarReservationServices
{
    private readonly ICarReservationReadRepository _carReservationReadRepository;
    private readonly ICarReservationWriteRepository _carReservationWriteRepository;
    private readonly IPickupLocationWriteRepository _pickupLocationWriteRepository;
    private readonly IReturnLocationWriteRepository _returnLocationWriteRepository;
    private readonly IMapper _mapper;

    public CarReservationServices(ICarReservationReadRepository carReservationReadRepository,
                                  ICarReservationWriteRepository carReservationWriteRepository,
                                  IMapper mapper,
                                  IPickupLocationWriteRepository pickupLocationWriteRepository,
                                  IReturnLocationWriteRepository returnLocationWriteRepository)
    {
        _carReservationReadRepository = carReservationReadRepository;
        _carReservationWriteRepository = carReservationWriteRepository;
        _mapper = mapper;
        _pickupLocationWriteRepository = pickupLocationWriteRepository;
        _returnLocationWriteRepository = returnLocationWriteRepository;
    }

    public async Task CreateAsync(CarReservationCreateDTO carReservationCreateDTO)
    {
        //var newReservation = _mapper.Map<CarReservation>(carReservationCreateDTO);
        var newReserv = new CarReservation
        {
            PickupDate = carReservationCreateDTO.PickupDate,
            ReturnDate = carReservationCreateDTO.ReturnDate,
            Notes = carReservationCreateDTO.Notes,
            Status = ReservationStatus.Pending,
            AppUserId = carReservationCreateDTO.AppUserId, //b9e6bbc7-b080-4405-880d-4aa8e35a5aee
            CarId = carReservationCreateDTO.CarId,
        };
        await _carReservationWriteRepository.AddAsync(newReserv);
        await _carReservationWriteRepository.SavaChangeAsync();

        newReserv.PickupLocation = new PickupLocation
        {
            CarReservationId = newReserv.Id,
            Latitude = carReservationCreateDTO.PickupLocation.Latitude,
            Longitude = carReservationCreateDTO.PickupLocation.Longitude
        };
        await _pickupLocationWriteRepository.AddAsync(newReserv.PickupLocation);

        newReserv.ReturnLocation = new ReturnLocation
        {
            CarReservationId = newReserv.Id,
            Latitude = carReservationCreateDTO.ReturnLocation.Latitude,
            Longitude = carReservationCreateDTO.ReturnLocation.Longitude
        };
        await _returnLocationWriteRepository.AddAsync(newReserv.ReturnLocation);
        await _carReservationWriteRepository.SavaChangeAsync();
    }

    public Task<List<CarReservationGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarReservationGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByReserv = await _carReservationReadRepository.GetByIdAsync(id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        _carReservationWriteRepository.Remove(ByReserv);
        await _carReservationWriteRepository.SavaChangeAsync();
    }

    public Task UpdateAsync(Guid id, CarReservationUpdateDTO carReservationUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
