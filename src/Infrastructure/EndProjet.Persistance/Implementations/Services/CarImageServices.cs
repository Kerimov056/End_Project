using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.Abstraction.Services.Stroge;
using EndProject.Application.DTOs.BlogImage;
using EndProject.Application.DTOs.CarImage;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Common;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.ExtensionsMethods;
using EndProjet.Persistance.Implementations.Repositories.EntityRepository;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class CarImageServices : ICarImageServices
{
    private readonly ICarImageReadRepository _carImageReadRepository;
    private readonly ICarImageWriteRepository _carImageWriteRepository;
    private readonly ICarReadRepository _carReadRepository;
    private readonly IStorageFile _storageFile;
    private readonly IMapper _mapper;

    public CarImageServices(ICarImageReadRepository carImageReadRepository,
                            ICarImageWriteRepository carImageWriteRepository,
                            IStorageFile storageFile,
                            IMapper mapper,
                            ICarReadRepository carReadRepository)
    {
        _carImageReadRepository = carImageReadRepository;
        _carImageWriteRepository = carImageWriteRepository;
        _storageFile = storageFile;
        _mapper = mapper;
        _carReadRepository = carReadRepository;
    }

    public async Task CreateAsync(CarImageCreateDTO carImageCreateDTO)
    {
        var ToEntity = _mapper.Map<CarImage>(carImageCreateDTO);
        if(carImageCreateDTO.image is not null)
        {
            ToEntity.imagePath = await carImageCreateDTO.image.GetBytes();
        }
        await _carImageWriteRepository.AddAsync(ToEntity);
        await _carImageWriteRepository.SavaChangeAsync();
    }

    public async Task<List<CarImageGetDTO>> GetAllAsync()
    {
        var CarImageAll = await _carImageReadRepository.
            GetAll()
           .Include(x => x.Car)
           .Where(x => x.Car.isReserv == false)
           .ToListAsync();
        if (CarImageAll is null) throw new NotFoundException("CarImage is Null");

        var ToDto = _mapper.Map<List<CarImageGetDTO>>(CarImageAll);
        foreach (var item in ToDto)
        {
            CarImage carImage = CarImageAll.FirstOrDefault(x => x.Id == item.Id)
                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

            List<string> images = new();
            images.Add(Convert.ToBase64String(carImage.imagePath));
            item.ImagePath = images[0];
        }
        return ToDto;
    }

    public async Task<List<CarImageGetDTO>> GetAllCarIdAsync(Guid carId)
    {
        var byCar = await _carReadRepository.GetByIdAsync(carId);
        if (byCar is null) throw new NotFoundException("Car Is Null");

        var CarImageAll = await _carImageReadRepository
                            .GetAll()
                            .Include(x => x.Car)
                            .Where(x => x.CarId == carId)
                            .ToListAsync();

        var carImageDto = _mapper.Map<List<CarImageGetDTO>>(CarImageAll);
        foreach (var item in carImageDto)
        {
            CarImage carImage =  CarImageAll.FirstOrDefault(x => x.Id == item.Id)
                                    ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

            List<string> images = new();
            images.Add(Convert.ToBase64String(carImage.imagePath));
            item.ImagePath = images[0];
        }
        return carImageDto;
    }

    public async Task<CarImageGetDTO> GetByIdAsync(Guid Id)
    {
        var ByCarImage = await _carImageReadRepository.GetByIdAsync(Id);
        if (ByCarImage is null) throw new NotFoundException("CarImage is Null");

        var ToDto = _mapper.Map<CarImageGetDTO>(ByCarImage);
        ToDto.ImagePath = Convert.ToBase64String(ByCarImage.imagePath);
        return ToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByCarImage = await _carImageReadRepository.GetByIdAsync(id);
        if (ByCarImage is null) throw new NotFoundException("CarImage is Null");

        _carImageWriteRepository.Remove(ByCarImage);
        await _carImageWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, CarImageUpdateDTO carImageUpdateDTO)
    {
        var ByCarImage = await _carImageReadRepository.GetByIdAsync(id);
        if (ByCarImage is null) throw new NotFoundException("CarImage is Null");

        _mapper.Map(carImageUpdateDTO, ByCarImage);
        if (carImageUpdateDTO.image is not null) ByCarImage.imagePath = await carImageUpdateDTO.image.GetBytes();
        _carImageWriteRepository.Update(ByCarImage);
        await _carImageWriteRepository.SavaChangeAsync();
    }
}
