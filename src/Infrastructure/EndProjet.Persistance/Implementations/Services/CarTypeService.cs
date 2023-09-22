using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarType;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class CarTypeService : ICarTypeService
{
    private readonly ICarTypeReadRepository _carTypeReadRepository;
    private readonly ICarTypeWriteRepository _carTypeWriteRepository;
    private readonly IMapper _mapper;

    public CarTypeService(ICarTypeReadRepository carTypeReadRepository,
                      ICarTypeWriteRepository carTypeWriteRepository,
                      IMapper mapper)
    {
        _carTypeReadRepository = carTypeReadRepository;
        _carTypeWriteRepository = carTypeWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CarTypeCreateDTO carTypeCreateDTO)
    {
        var newCarType = _mapper.Map<CarType>(carTypeCreateDTO);

        await _carTypeWriteRepository.AddAsync(newCarType);
        await _carTypeWriteRepository.SavaChangeAsync();
    }

    public async Task<List<CarTypeGetDTO>> GetAllAsync()
    {
        var TypeAll = await _carTypeReadRepository.GetAll().ToListAsync();
        if (TypeAll is null) throw new NotFoundException("Car Type is null");

        var ToDto = _mapper.Map<List<CarTypeGetDTO>>(TypeAll);
        return ToDto;
    }

    public async Task<CarTypeGetDTO> GetByIdAsync(Guid Id)
    {
        var ByCarType = await _carTypeReadRepository.GetByIdAsync(Id);
        if (ByCarType is null) throw new NotFoundException("Car Type is null");

        var ToDto = _mapper.Map<CarTypeGetDTO>(ByCarType);
        return ToDto;
    }

    public async Task<CarTypeGetDTO> GetByNameAsync(string type)
    {
        var ByCarType = await _carTypeReadRepository
            .GetByIdAsyncExpression(x=>x.type.ToLower()==type.ToLower());
        if (ByCarType is null) throw new NotFoundException("Car Type is null");

        var ToDto = _mapper.Map<CarTypeGetDTO>(ByCarType);
        return ToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var ByCarType = await _carTypeReadRepository.GetByIdAsync(id);
        if (ByCarType is null) throw new NotFoundException("Car Type is null");

        _carTypeWriteRepository.Remove(ByCarType);
        await _carTypeWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, CarTypeUpdateDTO carTypeUpdateDTO)
    {
        var ByCarType = await _carTypeReadRepository.GetByIdAsync(id);
        if (ByCarType is null) throw new NotFoundException("Car Type is null");

        _mapper.Map(carTypeUpdateDTO, ByCarType);
        _carTypeWriteRepository.Update(ByCarType);
        await _carTypeWriteRepository.SavaChangeAsync();
    }
}
