using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Application.DTOs.Slider;
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
              .Include(x => x.PickupLocation)
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

        if (carReservationUpdateDTO.ImagePath != null && carReservationUpdateDTO.ImagePath.Length > 0)
        {
            var ImagePath = await _uploadFile.WriteFile("Upload\\Files", carReservationUpdateDTO.ImagePath);
            ByReserv.ImagePath = ImagePath;
        }

        if (carReservationUpdateDTO.PickupDate < DateTime.Now) throw new Exception("Choose a Time !!!");
        if (carReservationUpdateDTO.ReturnDate < carReservationUpdateDTO.PickupDate) throw new Exception("Choose a Time !!! ");

        ByReserv.PickupDate = carReservationUpdateDTO.PickupDate;
        ByReserv.ReturnDate = carReservationUpdateDTO.ReturnDate;

        ByReserv.Notes = carReservationUpdateDTO.Notes;
        ByReserv.AppUserId = carReservationUpdateDTO.AppUserId;
        ByReserv.CarId = carReservationUpdateDTO.CarId;
        ByReserv.ChauffeursId = carReservationUpdateDTO.ChauffeursId;

        ByReserv.Status = ReservationStatus.Pending;

        if (ByReserv.PickupLocation is not null)
        {
            ByReserv.PickupLocation.Latitude = carReservationUpdateDTO.PickupLocation.Latitude;
            ByReserv.PickupLocation.Longitude = carReservationUpdateDTO.PickupLocation.Longitude;
        }
        else
        {
            ByReserv.PickupLocation = new PickupLocation
            {
                CarReservationId = ByReserv.Id,
                Latitude = carReservationUpdateDTO.PickupLocation.Latitude,
                Longitude = carReservationUpdateDTO.PickupLocation.Longitude
            };
            await _pickupLocationWriteRepository.AddAsync(ByReserv.PickupLocation);
        }

        if (ByReserv.ReturnLocation is not null)
        {
            ByReserv.ReturnLocation.Latitude = carReservationUpdateDTO.ReturnLocation.Latitude;
            ByReserv.ReturnLocation.Longitude = carReservationUpdateDTO.ReturnLocation.Longitude;
        }
        else
        {
            ByReserv.ReturnLocation = new ReturnLocation
            {
                CarReservationId = ByReserv.Id,
                Latitude = carReservationUpdateDTO.ReturnLocation.Latitude,
                Longitude = carReservationUpdateDTO.ReturnLocation.Longitude
            };
            await _returnLocationWriteRepository.AddAsync(ByReserv.ReturnLocation);
        }

        _carReservationWriteRepository.Update(ByReserv);
        await _carReservationWriteRepository.SavaChangeAsync();  //bidene burda gelir partlayir ifso.
    }
}
    //R      de244236-515b-44bc-d1ec-08dba0162b17
    //A      b9e6bbc7-b080-4405-880d-4aa8e35a5aee
    //C      1a0793e2-7158-4dd4-d60b-08dba011c904



