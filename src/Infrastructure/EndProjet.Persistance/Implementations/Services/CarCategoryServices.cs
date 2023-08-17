using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarType;
using EndProject.Application.DTOs.Category;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using EndProjet.Persistance.Implementations.Repositories.EntityRepository;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class CarCategoryServices : ICarCategoryServices
{
    private readonly ICarCategoryReadRepository _carCategoryReadRepository;
    private readonly ICarCategoryWriteRepository _carCategoryWriteRepository;
    private readonly IMapper _mapper;

    public CarCategoryServices(ICarCategoryReadRepository carCategoryReadRepository,
                               ICarCategoryWriteRepository carCategoryWriteRepository,
                               IMapper mapper)
    {
        _carCategoryReadRepository = carCategoryReadRepository;
        _carCategoryWriteRepository = carCategoryWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CarCategoryCreateDTO carCategoryCreateDTO)
    {
        var newCarType = _mapper.Map<CarCategory>(carCategoryCreateDTO);

        await _carCategoryWriteRepository.AddAsync(newCarType);
        await _carCategoryWriteRepository.SavaChangeAsync();
    }

    public async Task<List<CarCategoryGetDTO>> GetAllAsync()
    {
        var CategoryAll = await _carCategoryReadRepository.GetAll().ToListAsync();
        if (CategoryAll is null) throw new NotFoundException("Car Type is null");

        var ToDto = _mapper.Map<List<CarCategoryGetDTO>>(CategoryAll);
        return ToDto;
    }

    public async Task<CarCategoryGetDTO> GetByIdAsync(Guid Id)
    {
        var ByCarCategory = await _carCategoryReadRepository.GetByIdAsync(Id);
        if (ByCarCategory is null) throw new NotFoundException("Car Type is null");

        var ToDto = _mapper.Map<CarCategoryGetDTO>(ByCarCategory);
        return ToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByCarCategory = await _carCategoryReadRepository.GetByIdAsync(id);
        if (ByCarCategory is null) throw new NotFoundException("Car Type is null");

        _carCategoryWriteRepository.Remove(ByCarCategory);
        await _carCategoryWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, CarCategoryUpdateDTO carCategoryUpdateDTO)
    {
        var ByCarCategory = await _carCategoryReadRepository.GetByIdAsync(id);
        if (ByCarCategory is null) throw new NotFoundException("Car Type is null");

        _mapper.Map(carCategoryUpdateDTO, ByCarCategory);
        _carCategoryWriteRepository.Update(ByCarCategory);
        await _carCategoryWriteRepository.SavaChangeAsync();
    }
}
