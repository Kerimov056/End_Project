using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.CarType;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;

namespace EndProjet.Persistance.Implementations.Services;

public class CarTypeService : ICarTypeService
{
    private readonly ICarTypeReadRepository _carTypeReadRepository;
    private readonly ICarTypeWriteRepository _carTypeWriteRepository;

    public CarTypeService(ICarTypeReadRepository carTypeReadRepository,
                      ICarTypeWriteRepository carTypeWriteRepository)
    {
        _carTypeReadRepository = carTypeReadRepository;
        _carTypeWriteRepository = carTypeWriteRepository;
    }

    public async Task CreateAsync(CarTypeCreateDTO carTypeCreateDTO)
    {
        var ByType = await _carTypeReadRepository
            .GetByIdAsyncExpression(x=>x.type.ToLower().Equals(carTypeCreateDTO.Type));
        if (ByType is not null) throw new DublicatedException("Dubilcated Car Type Name!");


        var newCarType = new CarType
        {
            type = carTypeCreateDTO.Type,
        };

        await _carTypeWriteRepository.AddAsync(newCarType);
        await _carTypeWriteRepository.SavaChangeAsync();
    }

    public Task<List<CarTypeGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, CarTypeUpdateDTO carTypeUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
