using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarImage;
using EndProject.Application.DTOs.CarType;
using EndProject.Application.DTOs.Category;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class CarServices : ICarServices
{
    private readonly ICarReadRepository _carReadRepository;
    private readonly ICarWriteRepository _carWriteRepository;
    private readonly ICarTypeService _carTypeService;
    private readonly ICarTypeWriteRepository _carTypeWriteRepository;
    private readonly ICarImageServices _carImageServices;
    private readonly IMapper _mapper;
    private readonly ICarCategoryWriteRepository _carCategoryWriteRepository;
    private readonly ITagReadRepository _tagReadRepository;
    private readonly ITagWriteRepository _tagWriteRepository;
    private readonly ICarTagWriteRepository _carTagWriteRepository;
    private readonly ICarTagReadRepository _carTagReadRepository;
    private readonly ICarCategoryServices _carCategoryServices;

    public CarServices(ICarReadRepository carReadRepository,
                       ICarWriteRepository carWriteRepository,
                       IMapper mapper,
                       ICarTypeService carTypeService,
                       ICarTypeWriteRepository carTypeWriteRepository,
                       ICarImageServices carImageServices,
                       ITagReadRepository tagReadRepository,
                       ITagWriteRepository tagWriteRepository,
                       ICarCategoryWriteRepository carCategoryWriteRepository,
                       ICarTagWriteRepository carTagWriteRepository,
                       ICarTagReadRepository carTagReadRepository)
    {
        _carReadRepository = carReadRepository;
        _carWriteRepository = carWriteRepository;
        _mapper = mapper;
        _carTypeService = carTypeService;
        _carTypeWriteRepository = carTypeWriteRepository;
        _carImageServices = carImageServices;
        _tagReadRepository = tagReadRepository;
        _tagWriteRepository = tagWriteRepository;
        _carCategoryWriteRepository = carCategoryWriteRepository;
        _carTagWriteRepository = carTagWriteRepository;
        _carTagReadRepository = carTagReadRepository;
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

        newCar.carCategory = new CarCategory
        {
            Category = carCreateDTO.CarCategory.Category,
            CarId = newCar.Id
        };
        await _carCategoryWriteRepository.AddAsync(newCar.carCategory);

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

        foreach (var item in carCreateDTO.tags)
        {
            bool istag = true;
            foreach (var itemtag in await _tagReadRepository.GetAll().ToListAsync())
            {
                if (item == itemtag.tag)
                {
                    istag = false;
                    var newCarTag = new CarTag
                    {
                        CarId = newCar.Id,
                        Tag = itemtag
                    };
                    await _carTagWriteRepository.AddAsync(newCarTag);
                    await _carTagWriteRepository.SavaChangeAsync();
                    return;
                }
            }
            if (istag == true)
            {
                var newTag = new Tag
                {
                    tag = item
                };
                await _tagWriteRepository.AddAsync(newTag);
                await _tagWriteRepository.SavaChangeAsync();
            }
        }
        await _carWriteRepository.SavaChangeAsync();
    }

    public async Task<List<CarGetDTO>> GetAllAsync()
    {
        var CarAll = await _carReadRepository
            .GetAll()
            .Include(x => x.carTags)
            .Include(x => x.carType)
            .Include(x => x.carCategory)
            .Include(x => x.carImages)
            .Include(x => x.Reservations)
            .Where(x => x.isReserv == false)
            .ToListAsync();
        if (CarAll is null) throw new NotFoundException("Car is null");

        var ToDto = _mapper.Map<List<CarGetDTO>>(CarAll);
        return ToDto;
    }

    public async Task<CarGetDTO> GetByIdAsync(Guid Id)
    {
        var ByCar = await _carReadRepository
            .GetAll()
            .Include(x => x.carTags)
            .Include(x => x.carType)
            .Include(x => x.carCategory)
            .Include(x => x.carImages)
            .Include(x => x.Reservations)
            .FirstOrDefaultAsync(x => x.Id == Id);
        if (ByCar is null) throw new NotFoundException("Car is Null");

        var ToDto = _mapper.Map<CarGetDTO>(ByCar);
        return ToDto;
    }

    public async Task<List<CarGetDTO>> GetByNameAsync(string car)
    {
        var ByCar = await _carReadRepository
           .GetAll()
           .Include(x => x.carTags)
           .Include(x => x.carType)
           .Include(x => x.carCategory)
           .Include(x => x.carImages)
           .Include(x => x.Reservations)
           .Where(x => x.Marka == car)
           .ToListAsync();

        if (ByCar is null) throw new NotFoundException("Car is Null");

        var ToDto = _mapper.Map<List<CarGetDTO>>(ByCar);
        return ToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByCar = await _carReadRepository
           .GetAll()
           .Include(x => x.carTags)
           .Include(x => x.carType)
           .Include(x => x.carCategory)
           .Include(x => x.carImages)
           .Include(x => x.Reservations)
           .FirstOrDefaultAsync(x => x.Id == id);

        if (ByCar is null) throw new NotFoundException("Car is Null");

        _carWriteRepository.Remove(ByCar);
        await _carWriteRepository.SavaChangeAsync();
    }

    public async Task ReservCarFalse(Guid Id)
    {
        var ByCar = await _carReadRepository.GetByIdAsync(Id);
        if (ByCar is null) throw new NotFoundException("Car is Null");

        ByCar.isReserv = false;
        _carWriteRepository.Update(ByCar);  //False
        await _carWriteRepository.SavaChangeAsync();
    }

    public async Task ReservCarTrue(Guid Id)
    {
        var ByCar = await _carReadRepository.GetByIdAsync(Id);
        if (ByCar is null) throw new NotFoundException("Car is Null");

        ByCar.isReserv = true;
        _carWriteRepository.Update(ByCar);
        await _carWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, CarUpdateDTO carUpdateDTO)
    {
        var ByCar = await _carReadRepository
           .GetAll()                    //2bab2a3b-6435-48c5-f86d-08db9f6de8f1
           .Include(x => x.carTags)
           .Include(x => x.carType)
           .Include(x => x.carCategory)
           .Include(x => x.carImages)
           .Include(x => x.Reservations)
           .FirstOrDefaultAsync(x => x.Id == id);
        if (ByCar is null) throw new NotFoundException("Car is Null");

        ByCar.Marka = carUpdateDTO.Marka;
        ByCar.Model = carUpdateDTO.Model;
        ByCar.Price = carUpdateDTO.Price;
        ByCar.Year = carUpdateDTO.Year;
        ByCar.Description = carUpdateDTO.Description;
        ByCar.isReserv = carUpdateDTO.isReserv;
        if (ByCar.carType is not null)
        {
            var carUpdateType = new CarTypeUpdateDTO
            {
                Type = carUpdateDTO.CarType.Type
            };
            var CarTypeID = ByCar.carType.Id;
            _carTypeService.UpdateAsync(CarTypeID, carUpdateType);
        }
        else
        {
            ByCar.carType = new CarType
            {
                type = carUpdateDTO.CarType.Type,
                CarId = ByCar.Id
            };
            await _carTypeWriteRepository.AddAsync(ByCar.carType);
        }

        if (ByCar.carCategory is not null)
        {
            var carUpdateCategory = new CarCategoryUpdateDTO
            {
                category = carUpdateDTO.CarCategory.category
            };
            var CarCategoryID = ByCar.carCategory.Id;
            _carCategoryServices.UpdateAsync(CarCategoryID, carUpdateCategory);
        }
        else
        {
            ByCar.carCategory = new CarCategory
            {
                Category = carUpdateDTO.CarCategory.category,
                CarId = ByCar.Id
            };
            await _carCategoryWriteRepository.AddAsync(ByCar.carCategory);
        }

        foreach (var item in carUpdateDTO.tags)
        {
            var newTag = new Tag
            {
                tag = item
            };
            await _tagWriteRepository.AddAsync(newTag);
            await _tagWriteRepository.SavaChangeAsync();
            foreach (var tag in await _carTagReadRepository.GetAll().Where(x=>x.CarId==ByCar.Id).ToListAsync())
            {
                tag.TagId = newTag.Id;
                _carTagWriteRepository.Update(tag);
            }
        }

        if (carUpdateDTO.CarImages is not null)
        {
            foreach (var item in carUpdateDTO.CarImages)
            {
                if (ByCar.carImages is null)
                {
                    var carImageCreateDto = new CarImageCreateDTO
                    {
                        CarId = ByCar.Id,
                        image = item
                    };
                    await _carImageServices.CreateAsync(carImageCreateDto);
                }
                else
                {
                    var carImageDto = new CarImageUpdateDTO
                    {
                        CarId = ByCar.Id,
                        image = item
                    };
                    foreach (var updateImage in ByCar.carImages)
                    {
                        await _carImageServices.UpdateAsync(updateImage.Id, carImageDto);
                    }
                }
            }
        }

        _carWriteRepository.Update(ByCar);
        await _carWriteRepository.SavaChangeAsync();
    }
}
