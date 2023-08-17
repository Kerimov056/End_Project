﻿using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarImage;
using EndProject.Application.DTOs.CarType;
using EndProject.Domain.Entitys;
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

    public CarServices(ICarReadRepository carReadRepository,
                       ICarWriteRepository carWriteRepository,
                       IMapper mapper,
                       ICarTypeService carTypeService,
                       ICarTypeWriteRepository carTypeWriteRepository,
                       ICarImageServices carImageServices,
                       ITagReadRepository tagReadRepository,
                       ITagWriteRepository tagWriteRepository,
                       ICarCategoryWriteRepository carCategoryWriteRepository,
                       ICarTagWriteRepository carTagWriteRepository)
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
                if (item==itemtag.tag)
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
            if (istag==true)
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
