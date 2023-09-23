using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Trip;

namespace EndProjet.Persistance.Implementations.Services;

public class TripServices : ITripServices
{
    private readonly ITripeReadRepository _tripReadRepository;
    private readonly ITripeWriteRepository _tripWriteRepository;
    private readonly IMapper _mapper;

    public TripServices(ITripeReadRepository tripReadRepository,
                        ITripeWriteRepository tripWriteRepository,
                        IMapper mapper)
    {
        _tripReadRepository = tripReadRepository;
        _tripWriteRepository = tripWriteRepository;
        _mapper = mapper;
    }

    public Task CreateAsync(TripCreateDTO tripCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<TripGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TripGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, TripUpdateDTO tripUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
