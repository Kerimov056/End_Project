using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Communication;
using EndProject.Domain.Entitys;
using EndProjet.Persistance.Exceptions;
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

    public async Task<CommunicationGetDTO> GetByIdAsync(Guid Id)
    {
        var byCommunication = await _CommunicationReadRepository.GetByIdAsync(Id);
        if (byCommunication is null) throw new NotFoundException("Communication is null");
        var byEntityToDto = _mapper.Map<CommunicationGetDTO>(byCommunication);
        return byEntityToDto;
    }

    public async Task RemoveAsync(Guid Id)
    {
        var byCommunication = await _CommunicationReadRepository.GetByIdAsync(Id);
        if (byCommunication is null) throw new NotFoundException("Communication is null");
        _CommunicationWriteRepository.Remove(byCommunication);
        await _CommunicationWriteRepository.SavaChangeAsync();  
    }
}
