using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Communication;
using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class CommunicationServices : ICommunicationServices
{
    private readonly ICommunicationReadRepository _CommunicationReadRepository;
    private readonly ICommunicationWriteRepository _CommunicationWriteRepository;
    private readonly IMapper _mapper;

    public CommunicationServices(ICommunicationReadRepository communicationReadRepository,
                                 ICommunicationWriteRepository communicationWriteRepository,
                                 IMapper mapper)
    {
        _CommunicationReadRepository = communicationReadRepository;
        _CommunicationWriteRepository = communicationWriteRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(CommunicationCreateDTO communicationCreateDTO)
    {
        var newCommunucation = _mapper.Map<Communication>(communicationCreateDTO);
        await _CommunicationWriteRepository.AddAsync(newCommunucation);
        await _CommunicationWriteRepository.SavaChangeAsync();
    }

    public async Task<List<CommunicationGetDTO>> GetAllAsync()
    {
        var allCommunications = await _CommunicationReadRepository.GetAll().ToListAsync();
        var entityToDto = _mapper.Map<List<CommunicationGetDTO>>(allCommunications);
        return entityToDto;
    }

    public Task<CommunicationGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
