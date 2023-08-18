using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Domain.Entitys;
using EndProject.Domain.Enums.ReservationStatus;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class CarReservationServices : ICarReservationServices
{
    private readonly ICarReservationReadRepository _carReservationReadRepository;
    private readonly ICarReservationWriteRepository _carReservationWriteRepository;
    private readonly IPickupLocationWriteRepository _pickupLocationWriteRepository;
    private readonly IReturnLocationWriteRepository _returnLocationWriteRepository;
    private readonly ICarServices _carServices;
    private readonly IStorageFile _uploadFile;
    private readonly IMapper _mapper;

    public CarReservationServices(ICarReservationReadRepository carReservationReadRepository,
                                  ICarReservationWriteRepository carReservationWriteRepository,
                                  IMapper mapper,
                                  IPickupLocationWriteRepository pickupLocationWriteRepository,
                                  IReturnLocationWriteRepository returnLocationWriteRepository,
                                  IStorageFile uploadFile,
                                  ICarServices carServices)
    {
        _carReservationReadRepository = carReservationReadRepository;
        _carReservationWriteRepository = carReservationWriteRepository;
        _mapper = mapper;
        _pickupLocationWriteRepository = pickupLocationWriteRepository;
        _returnLocationWriteRepository = returnLocationWriteRepository;
        _uploadFile = uploadFile;
        _carServices = carServices;
    }

    public async Task CreateAsync(CarReservationCreateDTO carReservationCreateDTO)
    {
        if (carReservationCreateDTO.PickupDate < DateTime.Now) throw new Exception("Choose a Time !!!");
        if (carReservationCreateDTO.ReturnDate < carReservationCreateDTO.PickupDate) throw new Exception("Choose a Time !!! ");

        var newReserv = new CarReservation
        {
            PickupDate = carReservationCreateDTO.PickupDate,
            ReturnDate = carReservationCreateDTO.ReturnDate,
            Notes = carReservationCreateDTO.Notes,
            Status = ReservationStatus.Pending,
            AppUserId = carReservationCreateDTO.AppUserId, //b9e6bbc7-b080-4405-880d-4aa8e35a5aee
            CarId = carReservationCreateDTO.CarId,
        };
        await _carServices.ReservCar(carReservationCreateDTO.CarId);
        if (carReservationCreateDTO.Image != null && carReservationCreateDTO.Image.Length > 0)
        {
            var ImagePath = await _uploadFile.WriteFile("Upload\\Files", carReservationCreateDTO.Image);
            newReserv.ImagePath = ImagePath;
        }
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

    public async Task<List<CarReservationGetDTO>> GetAllAsync()
    {
        var ByReserv = await _carReservationReadRepository
              .GetAll()
              .Include(x => x.PickupLocation)    //de244236-515b-44bc-d1ec-08dba0162b17
              .Include(x => x.ReturnLocation)
              .ToListAsync();
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<CarReservationGetDTO>>(ByReserv);
        return ToDto;
    }

    public async Task<CarReservationGetDTO> GetByIdAsync(Guid Id)
    {
        var ByReserv = await _carReservationReadRepository
             .GetAll()
             .Include(x => x.PickupLocation)    //de244236-515b-44bc-d1ec-08dba0162b17
             .Include(x => x.ReturnLocation)
             .FirstOrDefaultAsync(x => x.Id == Id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<CarReservationGetDTO>(ByReserv);
        return ToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByReserv = await _carReservationReadRepository.GetByIdAsync(id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        _carReservationWriteRepository.Remove(ByReserv);
        await _carReservationWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, CarReservationUpdateDTO carReservationUpdateDTO)
    {
        var ByReserv = await _carReservationReadRepository.GetByIdAsync(id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        throw new NotImplementedException();
    }
}



//var ToDto = new CarReservationGetDTO
//{
//    ImagePath = ByReserv.ImagePath,
//    Id = ByReserv.Id,
//    PickupDate = ByReserv.PickupDate,
//    ReturnDate = ByReserv.ReturnDate,
//    Notes = ByReserv.Notes,
//    Status = ByReserv.Status,
//    AppUserId = ByReserv.AppUserId,
//    CarId = ByReserv.CarId,
//    ChauffeursId = ByReserv.ChauffeursId
//};
//var test = ByReserv.PickupLocation.Id;
//ToDto.PickupLocation.Id = test;

//ToDto.PickupLocation.Latitude = ByReserv.PickupLocation.Latitude;
//ToDto.PickupLocation.Longitude = ByReserv.PickupLocation.Longitude;

//ToDto.ReturnLocation.Latitude = ByReserv.ReturnLocation.Latitude;
//ToDto.ReturnLocation.Longitude = ByReserv.ReturnLocation.Longitude;