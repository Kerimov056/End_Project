using EndProject.Application.DTOs.Communication;

namespace EndProject.Application.Abstraction.Services;

public interface ICommunicationServices
{
    Task<List<CommunicationGetDTO>> GetAllAsync(string? searchUser);
    Task CreateAsync(CommunicationCreateDTO communicationCreateDTO);
    Task<CommunicationGetDTO> GetByIdAsync(Guid Id);
    Task RemoveAsync(Guid Id);
}
