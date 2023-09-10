using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Communication;

namespace EndProjet.Persistance.Implementations.Services;

public class CommunicationServices : ICommunicationServices
{
    public Task CreateAsync(CommunicationCreateDTO communicationCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<CommunicationGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
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
