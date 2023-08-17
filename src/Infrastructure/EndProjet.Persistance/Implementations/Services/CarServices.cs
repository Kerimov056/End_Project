using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarImage;
using EndProject.Application.DTOs.CarType;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.Implementations.Services;

public class CarServices : ICarServices
{
    private readonly ICarReadRepository _carReadRepository;
    private readonly ICarWriteRepository _carWriteRepository;
    private readonly ICarTypeService _carTypeService;
    private readonly ICarTypeWriteRepository _carTypeWriteRepository;
    private readonly ICarImageServices _carImageServices;
    private readonly IMapper _mapper;

    public CarServices(ICarReadRepository carReadRepository,
                       ICarWriteRepository carWriteRepository,
                       IMapper mapper,
                       ICarTypeService carTypeService,
                       ICarTypeWriteRepository carTypeWriteRepository,
                       ICarImageServices carImageServices)
    {
        _carReadRepository = carReadRepository;
        _carWriteRepository = carWriteRepository;
        _mapper = mapper;
        _carTypeService = carTypeService;
        _carTypeWriteRepository = carTypeWriteRepository;
        _carImageServices = carImageServices;
    }

    public async Task CreateAsync(CarCreateDTO carCreateDTO)
    {
        var newCar = new Car
        {
            Marka = carCreateDTO.Marka,
            Model = carCreateDTO.Model,
            Price = carCreateDTO.Price,
            Description = carCreateDTO.Description,
            Year = carCreateDTO.Year
        };

        await _carWriteRepository.AddAsync(newCar);
        await _carWriteRepository.SavaChangeAsync();

        newCar.carType = new CarType
        {
            type = carCreateDTO.CarType.type,
            CarId = newCar.Id
        };
        await _carTypeWriteRepository.AddAsync(newCar.carType);

        if (carCreateDTO.CarImages is not null)
        {
            foreach (var item in carCreateDTO.CarImages)
            {
                var carImageDto = new CarImageCreateDTO
                {
                    CarId = newCar.Id,
                    image = item
                };
                await _carImageServices.CreateAsync(carImageDto);
            }
        }
    }

    public Task<List<CarGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<CarGetDTO> GetByNameAsync(string car)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, CarUpdateDTO carUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
