using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Common;
using EndProject.Domain.Enums.ReservationStatus;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.ExtensionsMethods;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class CarReservationServices : ICarReservationServices
{
    private readonly ICarReservationReadRepository _carReservationReadRepository;
    private readonly ICarReservationWriteRepository _carReservationWriteRepository;
    private readonly IPickupLocationWriteRepository _pickupLocationWriteRepository;
    private readonly IReturnLocationWriteRepository _returnLocationWriteRepository;
    private readonly ICarServices _carServices;
    private readonly IChauffeursServices _chauffeursServices;
    private readonly IStorageFile _uploadFile;
    private readonly IMapper _mapper;
    private readonly IBasketServices _basketServices;

    public CarReservationServices(ICarReservationReadRepository carReservationReadRepository,
                                  ICarReservationWriteRepository carReservationWriteRepository,
                                  IMapper mapper,
                                  IPickupLocationWriteRepository pickupLocationWriteRepository,
                                  IReturnLocationWriteRepository returnLocationWriteRepository,
                                  IStorageFile uploadFile,
                                  ICarServices carServices,
                                  IChauffeursServices chauffeursServices,
                                  IBasketServices basketServices)
    {
        _carReservationReadRepository = carReservationReadRepository;
        _carReservationWriteRepository = carReservationWriteRepository;
        _mapper = mapper;
        _pickupLocationWriteRepository = pickupLocationWriteRepository;
        _returnLocationWriteRepository = returnLocationWriteRepository;
        _uploadFile = uploadFile;
        _carServices = carServices;
        _chauffeursServices = chauffeursServices;
        _basketServices = basketServices;
    }

    public async Task AllCreateAsync(AllCarReservation AllCarReservation)
    {
        var products = await _basketServices.GetBasketProductsAsync(AllCarReservation.AppUserId);
        foreach (var product in products)
        {
            CarReservationCreateDTO carCreateDTO = new()
            {
                FullName = AllCarReservation.FullName,
                Image = AllCarReservation.Image,
                Email = AllCarReservation.Email,
                Notes = AllCarReservation.Notes,
                Number = AllCarReservation.Number,
                PickupDate = AllCarReservation.PickupDate,
                ReturnDate = AllCarReservation.ReturnDate,
                AppUserId = AllCarReservation.AppUserId,
                CarId = product.CarId,
                PickupLocation = AllCarReservation.PickupLocation,
                ReturnLocation = AllCarReservation.ReturnLocation,
                ChauffeursId = null
            };

            await CreateAsync(carCreateDTO);
        }
    }


    public async Task CreateAsync(CarReservationCreateDTO carReservationCreateDTO)
    {
        if (carReservationCreateDTO.PickupDate < DateTime.Now) throw new Exception("Choose a Time !!!");
        if (carReservationCreateDTO.ReturnDate <= carReservationCreateDTO.PickupDate) throw new Exception("Choose a Time !!! ");

        DateTime minimumReturnDate = carReservationCreateDTO.PickupDate.AddDays(1);
        if (carReservationCreateDTO.ReturnDate < minimumReturnDate) throw new Exception("ReturnDate must be at least 1 day after PickupDate.");
        var newReserv = new CarReservation
        {
            FullName = carReservationCreateDTO.FullName,
            Email = carReservationCreateDTO.Email,
            Number = carReservationCreateDTO.Number,
            PickupDate = carReservationCreateDTO.PickupDate,
            ReturnDate = carReservationCreateDTO.ReturnDate,
            Notes = carReservationCreateDTO.Notes,
            Status = ReservationStatus.Pending,
            AppUserId = carReservationCreateDTO.AppUserId,
            CarId = carReservationCreateDTO.CarId,
            ChauffeursId = carReservationCreateDTO.ChauffeursId
        };
        //var userId = "4d12fb35-a688-4270-9c51-f28d4b19e3ae";


        if (carReservationCreateDTO.Image is not null)
        {
            newReserv.ImagePath = await carReservationCreateDTO.Image.GetBytes();
        }
        await _carReservationWriteRepository.AddAsync(newReserv);
        await _carReservationWriteRepository.SavaChangeAsync();

        if (carReservationCreateDTO.PickupLocation.Latitude is not null)
        {

            newReserv.PickupLocation = new PickupLocation
            {
                CarReservationId = newReserv.Id,
                Latitude = (double)carReservationCreateDTO.PickupLocation.Latitude,
                Longitude = (double)carReservationCreateDTO.PickupLocation.Longitude
            };
            await _pickupLocationWriteRepository.AddAsync(newReserv.PickupLocation);
        }

        if (carReservationCreateDTO.ReturnLocation.Latitude is not null)
        {
            newReserv.ReturnLocation = new ReturnLocation
            {
                CarReservationId = newReserv.Id,
                Latitude = (double)carReservationCreateDTO.ReturnLocation.Latitude,
                Longitude = (double)carReservationCreateDTO.ReturnLocation.Longitude
            };
            await _returnLocationWriteRepository.AddAsync(newReserv.ReturnLocation);
        }

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
        foreach (var byCar in ByReserv)
        {
            foreach (var byCarDto in ToDto)
            {
                CarReservation carReservation = ByReserv.FirstOrDefault(x => x.Id == byCarDto.Id)
                                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

                List<string> images = new();
                images.Add(Convert.ToBase64String(carReservation.ImagePath));
                byCarDto.ImagePath = images[0];

                if (byCar.Id == byCarDto.Id)
                {
                    byCarDto.ReservCar = await _carServices.GetByIdAsync(byCar.CarId);
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<CarReservationGetDTO> GetByIdAsync(Guid Id)
    {
        var ByReserv = await _carReservationReadRepository
             .GetAll()
             .Include(x => x.PickupLocation)
             .Include(x => x.ReturnLocation)
             .FirstOrDefaultAsync(x => x.Id == Id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<CarReservationGetDTO>(ByReserv);
        ToDto.ImagePath = Convert.ToBase64String(ByReserv.ImagePath);
        ToDto.ReservCar = await _carServices.GetByIdAsync(ByReserv.CarId);
        return ToDto;
    }

    public async Task<int> GetCanceledCountAsync()
    {
        return await _carReservationReadRepository.GetReservCanceledCountAsync();
    }

    public async Task<int> GetCompletedCountAsync()
    {
        return await _carReservationReadRepository.GetReservCompletedCountAsync();
    }

    public async Task<int> GetConfirmedCountAsync()
    {
        return await _carReservationReadRepository.GetReservConfirmedCountAsync();
    }

    public async Task<int> GetPeddingCountAsync()
    {
        return await _carReservationReadRepository.GetReservPeddingCountAsync();
    }

    public async Task<CarReservationGetDTO> GetReservValue(Guid CarId)
    {
        var Reserv = await _carReservationReadRepository
            .GetAll()
            .Where(x => x.CarId == CarId)
            .FirstOrDefaultAsync();

        var ToDto = _mapper.Map<CarReservationGetDTO>(Reserv);
        ToDto.ImagePath = Convert.ToBase64String(Reserv.ImagePath);

        return ToDto is null ? null : ToDto;
    }

    //private static ReservationStatus StatusNull() => null;

    public async Task<List<CarReservationGetDTO>> IsResevCanceledGetAll()
    {
        var ByReserv = await _carReservationReadRepository
             .GetAll()
             .Include(x => x.PickupLocation)
             .Include(x => x.ReturnLocation)
             .Where(x => x.Status == ReservationStatus.Canceled)
             .ToListAsync();
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<CarReservationGetDTO>>(ByReserv);
        foreach (var byCar in ByReserv)
        {
            foreach (var byCarDto in ToDto)
            {
                CarReservation carReservation = ByReserv.FirstOrDefault(x => x.Id == byCarDto.Id)
                                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

                List<string> images = new();
                images.Add(Convert.ToBase64String(carReservation.ImagePath));
                byCarDto.ImagePath = images[0];

                if (byCar.Id == byCarDto.Id)
                {
                    byCarDto.ReservCar = await _carServices.GetByIdAsync(byCar.CarId);
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<List<CarReservationGetDTO>> IsResevComplatedGetAll()
    {
        var ByReserv = await _carReservationReadRepository
             .GetAll()
             .Include(x => x.PickupLocation)
             .Include(x => x.ReturnLocation)
             .Where(x => x.Status == ReservationStatus.Completed)
             .ToListAsync();
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<CarReservationGetDTO>>(ByReserv);
        foreach (var byCar in ByReserv)
        {
            foreach (var byCarDto in ToDto)
            {
                CarReservation carReservation = ByReserv.FirstOrDefault(x => x.Id == byCarDto.Id)
                                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

                List<string> images = new();
                images.Add(Convert.ToBase64String(carReservation.ImagePath));
                byCarDto.ImagePath = images[0];

                if (byCar.Id == byCarDto.Id)
                {
                    byCarDto.ReservCar = await _carServices.GetByIdAsync(byCar.CarId);
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<List<CarReservationGetDTO>> IsResevConfirmedGetAll()
    {
        var ByReserv = await _carReservationReadRepository
             .GetAll()
             .Include(x => x.PickupLocation)
             .Include(x => x.ReturnLocation)
             .Where(x => x.Status == ReservationStatus.Confirmed)
             .ToListAsync();

        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<CarReservationGetDTO>>(ByReserv);
        foreach (var byCar in ByReserv)
        {
            foreach (var byCarDto in ToDto)
            {
                CarReservation carReservation = ByReserv.FirstOrDefault(x => x.Id == byCarDto.Id)
                                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

                List<string> images = new();
                images.Add(Convert.ToBase64String(carReservation.ImagePath));
                byCarDto.ImagePath = images[0];

                if (byCar.Id == byCarDto.Id)
                {
                    byCarDto.ReservCar = await _carServices.GetByIdAsync(byCar.CarId);
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<List<CarReservationGetDTO>> IsResevConfirmLocationGetAll()
    {

        var ByReserv = await _carReservationReadRepository
          .GetAll()
          .Include(x => x.PickupLocation)
          .Include(x => x.ReturnLocation)
          .Where(x => x.Status == ReservationStatus.Confirmed)
          .Where(x => x.PickupLocation != null)
          .Where(x => x.ReturnLocation != null)
          .ToListAsync();

        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<CarReservationGetDTO>>(ByReserv);
        foreach (var byCar in ByReserv)
        {
            foreach (var byCarDto in ToDto)
            {
                CarReservation carReservation = ByReserv.FirstOrDefault(x => x.Id == byCarDto.Id)
                                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

                List<string> images = new();
                images.Add(Convert.ToBase64String(carReservation.ImagePath));
                byCarDto.ImagePath = images[0];

                if (byCar.Id == byCarDto.Id)
                {
                    byCarDto.ReservCar = await _carServices.GetByIdAsync(byCar.CarId);
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<List<CarReservationGetDTO>> IsResevConfirmPickUpGetAll()
    {
        var ByReserv = await _carReservationReadRepository
            .GetAll()
            .Include(x => x.PickupLocation)
            .Include(x => x.ReturnLocation)
            .Where(x => x.Status == ReservationStatus.Confirmed)
            .Where(x => x.PickupLocation != null)
            .Where(x => x.ReturnLocation == null)
            .ToListAsync();

        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<CarReservationGetDTO>>(ByReserv);
        foreach (var byCar in ByReserv)
        {
            foreach (var byCarDto in ToDto)
            {
                CarReservation carReservation = ByReserv.FirstOrDefault(x => x.Id == byCarDto.Id)
                                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

                List<string> images = new();
                images.Add(Convert.ToBase64String(carReservation.ImagePath));
                byCarDto.ImagePath = images[0];

                if (byCar.Id == byCarDto.Id)
                {
                    byCarDto.ReservCar = await _carServices.GetByIdAsync(byCar.CarId);
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<List<CarReservationGetDTO>> IsResevConfirmReturnGetAll()
    {
        var ByReserv = await _carReservationReadRepository
           .GetAll()
           .Include(x => x.PickupLocation)
           .Include(x => x.ReturnLocation)
           .Where(x => x.Status == ReservationStatus.Confirmed)
           .Where(x => x.PickupLocation == null)
           .Where(x => x.ReturnLocation != null)
           .ToListAsync();

        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<CarReservationGetDTO>>(ByReserv);
        foreach (var byCar in ByReserv)
        {
            foreach (var byCarDto in ToDto)
            {
                CarReservation carReservation = ByReserv.FirstOrDefault(x => x.Id == byCarDto.Id)
                                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

                List<string> images = new();
                images.Add(Convert.ToBase64String(carReservation.ImagePath));
                byCarDto.ImagePath = images[0];

                if (byCar.Id == byCarDto.Id)
                {
                    byCarDto.ReservCar = await _carServices.GetByIdAsync(byCar.CarId);
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<List<CarReservationGetDTO>> IsResevNowGetAll()
    {
        var ByReserv = await _carReservationReadRepository
            .GetAll()
            .Include(x => x.PickupLocation)
            .Include(x => x.ReturnLocation)
            .Where(x => x.Status == ReservationStatus.RightNow)
            .ToListAsync();

        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<CarReservationGetDTO>>(ByReserv);
        foreach (var byCar in ByReserv)
        {
            foreach (var byCarDto in ToDto)
            {
                CarReservation carReservation = ByReserv.FirstOrDefault(x => x.Id == byCarDto.Id)
                                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

                List<string> images = new();
                images.Add(Convert.ToBase64String(carReservation.ImagePath));
                byCarDto.ImagePath = images[0];

                if (byCar.Id == byCarDto.Id)
                {
                    byCarDto.ReservCar = await _carServices.GetByIdAsync(byCar.CarId);
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task<List<CarReservationGetDTO>> IsResevPedingGetAll()
    {
        var ByReserv = await _carReservationReadRepository
             .GetAll()
             .Include(x => x.PickupLocation)
             .Include(x => x.ReturnLocation)
             .Where(x => x.Status == ReservationStatus.Pending)
             .ToListAsync();

        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<CarReservationGetDTO>>(ByReserv);
        foreach (var byCar in ByReserv)
        {
            foreach (var byCarDto in ToDto)
            {
                CarReservation carReservation = ByReserv.FirstOrDefault(x => x.Id == byCarDto.Id)
                                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

                List<string> images = new();
                images.Add(Convert.ToBase64String(carReservation.ImagePath));
                byCarDto.ImagePath = images[0];

                if (byCar.Id == byCarDto.Id)
                {
                    byCarDto.ReservCar = await _carServices.GetByIdAsync(byCar.CarId);
                    break;
                }
            }
        }
        return ToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByReserv = await _carReservationReadRepository.GetByIdAsync(id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        _carReservationWriteRepository.Remove(ByReserv);
        await _carReservationWriteRepository.SavaChangeAsync();
    }

    public async Task StatusCanceled(Guid Id)
    {
        var ByReserv = await _carReservationReadRepository.GetByIdAsync(Id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        ByReserv.Status = ReservationStatus.Canceled;
        _carReservationWriteRepository.Update(ByReserv);
        await _carReservationWriteRepository.SavaChangeAsync();
    }

    public async Task StatusCompleted(Guid reservId)
    {
        var ByReserv = await _carReservationReadRepository.GetByIdAsync(reservId);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        ByReserv.Status = ReservationStatus.Completed;
        _carReservationWriteRepository.Update(ByReserv);
        await _carReservationWriteRepository.SavaChangeAsync();
    }

    public async Task StatusConfirmed(Guid Id)
    {
        var ByReserv = await _carReservationReadRepository.GetByIdAsync(Id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        ByReserv.Status = ReservationStatus.Confirmed;

        _carReservationWriteRepository.Update(ByReserv);
        await _carReservationWriteRepository.SavaChangeAsync();
    }

    public async Task StatusNow(Guid Id)
    {
        var ByReserv = await _carReservationReadRepository.GetByIdAsync(Id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        ByReserv.Status = ReservationStatus.RightNow;

        _carReservationWriteRepository.Update(ByReserv);
        await _carReservationWriteRepository.SavaChangeAsync();
    }

    public Task SumCreateAsync(CarReservationCreateDTO carReservationCreateDTO)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Guid id, CarReservationUpdateDTO carReservationUpdateDTO)
    {
        var ByReserv = await _carReservationReadRepository.GetByIdAsync(id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        if (carReservationUpdateDTO.PickupDate < DateTime.Now) throw new Exception("Choose a Time !!!");
        if (carReservationUpdateDTO.ReturnDate < carReservationUpdateDTO.PickupDate) throw new Exception("Choose a Time !!! ");

        DateTime minimumReturnDate = carReservationUpdateDTO.PickupDate.AddDays(1);
        if (carReservationUpdateDTO.ReturnDate < minimumReturnDate) throw new Exception("ReturnDate must be at least 1 day after PickupDate.");

        if (carReservationUpdateDTO.ImagePath is not null) ByReserv.ImagePath = await carReservationUpdateDTO.ImagePath.GetBytes();

        ByReserv.FullName = carReservationUpdateDTO.FullName;
        ByReserv.PickupDate = carReservationUpdateDTO.PickupDate;
        ByReserv.ReturnDate = carReservationUpdateDTO.ReturnDate;
        ByReserv.Email = carReservationUpdateDTO.Email;
        ByReserv.Number = carReservationUpdateDTO.Number;

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
                                                                 //R      de244236-515b-44bc-d1ec-08dba0162b17
                                                                 //A      b9e6bbc7-b080-4405-880d-4aa8e35a5aee
                                                                 //C      1a0793e2-7158-4dd4-d60b-08dba011c904
    }

    public async Task<List<CarReservationGetDTO>> YouGetAllAsync(string Id)
    {
        var ByReserv = await _carReservationReadRepository
              .GetAll()
              .Include(x => x.PickupLocation)
              .Include(x => x.ReturnLocation)
              .Where(x => x.AppUserId == Id)
              .ToListAsync();
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        var ToDto = _mapper.Map<List<CarReservationGetDTO>>(ByReserv);
        foreach (var byCar in ByReserv)
        {
            foreach (var byCarDto in ToDto)
            {
                if (byCar.Id == byCarDto.Id)
                {
                    byCarDto.ReservCar = await _carServices.GetByIdAsync(byCar.CarId);
                    break;
                }
            }
        }
        return ToDto;
    }
}

