using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Advantage;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
using Microsoft.EntityFrameworkCore;

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

    public async Task CreateAsync(AdvantageCreateDTO advantageCreateDTO)
    {
        var ToEntity = _mapper.Map<Advantage>(advantageCreateDTO);
        await _writeRepository.AddAsync(ToEntity);
        await _writeRepository.SavaChangeAsync();
    }

    public async Task<List<AdvantageGetDTO>> GetAllAsync()
    {
        var AllAdvantage = await _readRepository.GetAll().ToListAsync();
        if (AllAdvantage is null) throw new NotFoundException("Advantage is Null");

        var ToDto = _mapper.Map<List<AdvantageGetDTO>>(AllAdvantage);
        return ToDto;
    }

    public async Task<AdvantageGetDTO> GetByIdAsync(Guid Id)
    {
        var byAdvantage = await _readRepository.GetByIdAsync(Id);
        if (byAdvantage is null) throw new NotFoundException("Advantage is Null");

        var ToDto = _mapper.Map<AdvantageGetDTO>(byAdvantage);
        return ToDto;
    }

    public async Task RemoveAsync(Guid id)
    {
        var byAdvantage = await _readRepository.GetByIdAsync(id);
        if (byAdvantage is null) throw new NotFoundException("Advantage is Null");

        _writeRepository.Remove(byAdvantage);
        await _writeRepository.SavaChangeAsync();
    }

    public Task UpdateAsync(Guid id, AdvantageUpdateDTO advantageUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
