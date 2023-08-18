using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.CarReservation;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.Implementations.Services;

public class CarReservationServices : ICarReservationServices
{
    private readonly ICarReservationReadRepository _carReservationReadRepository;
    private readonly ICarReservationWriteRepository _carReservationWriteRepository;
    private readonly IMapper _mapper;

    public CarReservationServices(ICarReservationReadRepository carReservationReadRepository,
                                  ICarReservationWriteRepository carReservationWriteRepository,
                                  IMapper mapper)
    {
        _carReservationReadRepository = carReservationReadRepository;
        _carReservationWriteRepository = carReservationWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CarReservationCreateDTO carReservationCreateDTO)
    {
        var newReservation = _mapper.Map<CarReservation>(carReservationCreateDTO);
        await _carReservationWriteRepository.AddAsync(newReservation);
        await _carReservationWriteRepository.SavaChangeAsync();
    }

    public Task<List<CarReservationGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarReservationGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, CarReservationUpdateDTO carReservationUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
