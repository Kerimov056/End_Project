using EndProject.Application.DTOs.Communication;
using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface ICommunicationServices
{
    Task<List<CommunicationGetDTO>> GetAllAsync();
    Task CreateAsync(CommunicationCreateDTO communicationCreateDTO);
    Task<CommunicationGetDTO> GetByIdAsync(Guid Id);
    Task RemoveAsync(Guid Id);
}
