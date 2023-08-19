using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Advantage;

namespace EndProjet.Persistance.Implementations.Services;

public class AdvantageServices : IAdvantageServices
{
    private readonly IAdvantageReadRepository _readRepository;
    private readonly IAdvantageWriteRepository _writeRepository;    
    private readonly IMapper _mapper;

    public AdvantageServices(IAdvantageReadRepository readRepository,
                             IAdvantageWriteRepository writeRepository,
                             IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public Task CreateAsync(AdvantageCreateDTO advantageCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<AdvantageGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AdvantageGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, AdvantageUpdateDTO advantageUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
