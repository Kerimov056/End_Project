using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.Abstraction.Services.Stroge;
using EndProject.Application.DTOs.OtherCarReservation;
using EndProject.Domain.Entitys;
using EndProject.Domain.Enums.ReservationStatus;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class OtherCarReservationServices : IOtherCarReservationServices
{
    private readonly IOtherCarReservationReadRepository _otherCarReservationRead;
    private readonly IOtherCarReservationWriteRepository _otherCarReservationWrite;
    private readonly IStorageFile _storageFile;
    private readonly ICarServices _carServices;
    private readonly IMapper _mapper;

    public OtherCarReservationServices(IOtherCarReservationReadRepository otherCarReservationRead,
                                       IOtherCarReservationWriteRepository otherCarReservationWrite,
                                       IMapper mapper,
                                       ICarServices carServices,
                                       IStorageFile storageFile)
    {
        _otherCarReservationRead = otherCarReservationRead;
        _otherCarReservationWrite = otherCarReservationWrite;
        _mapper = mapper;
        _carServices = carServices;
        _storageFile = storageFile;
    }

    public async Task CreateAsync(OtherCarReservationCreateDTO otherCarReservationCreateDTO)
    {
        if (otherCarReservationCreateDTO.PickupDate < DateTime.Now) throw new Exception("Choose a Time !!!");
        if (otherCarReservationCreateDTO.ReturnDate < otherCarReservationCreateDTO.PickupDate) throw new Exception("Choose a Time !!! ");

        DateTime minimumReturnDate = otherCarReservationCreateDTO.PickupDate.AddDays(1);
        if (otherCarReservationCreateDTO.ReturnDate < minimumReturnDate) throw new Exception("ReturnDate must be at least 1 day after PickupDate.");


        var ToEntity = _mapper.Map<OtherCarReservation>(otherCarReservationCreateDTO);
        if (otherCarReservationCreateDTO.ImagePath != null && otherCarReservationCreateDTO.ImagePath.Length > 0)
        {
            var ImagePath = await _storageFile.WriteFile("Upload\\Files", otherCarReservationCreateDTO.ImagePath);
            ToEntity.ImagePath = ImagePath;
        }
        if (otherCarReservationCreateDTO.PersonImage != null && otherCarReservationCreateDTO.PersonImage.Length > 0)
        {
            var ImagePath = await _storageFile.WriteFile("Upload\\Files", otherCarReservationCreateDTO.PersonImage);
            ToEntity.PersonImage = ImagePath;
        }
        ToEntity.Status = ReservationStatus.Pending;
        await _otherCarReservationWrite.AddAsync(ToEntity);
        await _otherCarReservationWrite.SavaChangeAsync();
    }

    public async Task<List<OtherCarReservationGetDTO>> GetAllAsync()
    {
        var ByReserv = await _otherCarReservationRead
             .GetAll()
             .ToListAsync();
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<OtherCarReservationGetDTO>>(ByReserv);
        return ToDto;
    }

    public async Task<OtherCarReservationGetDTO> GetByIdAsync(Guid Id)
    {
        var ByReserv = await _otherCarReservationRead
            .GetAll()
            .FirstOrDefaultAsync(x => x.Id == Id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<OtherCarReservationGetDTO>(ByReserv);
        return ToDto;
    }

    public async Task<List<OtherCarReservationGetDTO>> IsResevCanceledGetAll()
    {
        var ByReserv = await _otherCarReservationRead
            .GetAll()
            .Where(x => x.Status == ReservationStatus.Canceled)
            .ToListAsync();
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<OtherCarReservationGetDTO>>(ByReserv);
        return ToDto;
    }

    public async Task<List<OtherCarReservationGetDTO>> IsResevComplatedGetAll()
    {
        var ByReserv = await _otherCarReservationRead
            .GetAll()
            .Where(x => x.Status == ReservationStatus.Completed)
            .ToListAsync();
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<OtherCarReservationGetDTO>>(ByReserv);
        return ToDto;
    }

    public async Task<List<OtherCarReservationGetDTO>> IsResevConfirmedGetAll()
    {
        var ByReserv = await _otherCarReservationRead
            .GetAll()
            .Where(x => x.Status == ReservationStatus.Confirmed)
            .ToListAsync();
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<OtherCarReservationGetDTO>>(ByReserv);
        return ToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByReserv = await _otherCarReservationRead.GetByIdAsync(id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        _otherCarReservationWrite.Remove(ByReserv);
        await _otherCarReservationWrite.SavaChangeAsync();
    }

    public async Task StatusCanceled(Guid Id)
    {
        var ByReserv = await _otherCarReservationRead.GetByIdAsync(Id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        ByReserv.Status = ReservationStatus.Canceled;
        _otherCarReservationWrite.Update(ByReserv);
        await _otherCarReservationWrite.SavaChangeAsync();
    }

    public async Task StatusCompleted(Guid reservId)
    {
        var ByReserv = await _otherCarReservationRead.GetByIdAsync(reservId);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        ByReserv.Status = ReservationStatus.Completed;
        _otherCarReservationWrite.Update(ByReserv);
        await _otherCarReservationWrite.SavaChangeAsync();
    }

    public async Task StatusConfirmed(Guid Id)
    {
        var ByReserv = await _otherCarReservationRead.GetByIdAsync(Id);
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");

        ByReserv.Status = ReservationStatus.Confirmed;

        _otherCarReservationWrite.Update(ByReserv);
        await _carServices.ReservCarTrue(ByReserv.CarId);
    }

    public async Task UpdateAsync(Guid id, OtherCarReservationUpdateDTO otherCarReservationUpdateDTO)
    {
        var ByOtherCarReserv = await _otherCarReservationRead.GetByIdAsync(id);
        if (ByOtherCarReserv is null) throw new NotFoundException("Reservation is Null");

        if (otherCarReservationUpdateDTO.PickupDate < DateTime.Now) throw new Exception("Choose a Time !!!");
        if (otherCarReservationUpdateDTO.ReturnDate < otherCarReservationUpdateDTO.PickupDate) throw new Exception("Choose a Time !!! ");

        DateTime minimumReturnDate = otherCarReservationUpdateDTO.PickupDate.AddDays(1);
        if (otherCarReservationUpdateDTO.ReturnDate < minimumReturnDate) throw new Exception("ReturnDate must be at least 1 day after PickupDate.");

        _mapper.Map(otherCarReservationUpdateDTO, ByOtherCarReserv);
        if (otherCarReservationUpdateDTO.ImagePath != null && otherCarReservationUpdateDTO.ImagePath.Length > 0)
        {
            var ImagePath = await _storageFile.WriteFile("Upload\\Files", otherCarReservationUpdateDTO.ImagePath);
            ByOtherCarReserv.ImagePath = ImagePath;
        }
        if (otherCarReservationUpdateDTO.PersonImage != null && otherCarReservationUpdateDTO.PersonImage.Length > 0)
        {
            var ImagePath = await _storageFile.WriteFile("Upload\\Files", otherCarReservationUpdateDTO.PersonImage);
            ByOtherCarReserv.PersonImage = ImagePath;
        }
        _otherCarReservationWrite.Update(ByOtherCarReserv);
        await _otherCarReservationWrite.SavaChangeAsync();
    }

    public async Task<List<OtherCarReservationGetDTO>> YouGetAllAsync(string Id)
    {
        var ByReserv = await _otherCarReservationRead
              .GetAll()
              .Where(x => x.AppUserId == Id)
              .ToListAsync();
        if (ByReserv is null) throw new NotFoundException("Reservation is Null");
        var ToDto = _mapper.Map<List<OtherCarReservationGetDTO>>(ByReserv);
        return ToDto;
    }
}
