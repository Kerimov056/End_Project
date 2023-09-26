using EndProject.Application.DTOs.ShareTrip;

namespace EndProject.Application.Abstraction.Services;

public interface IShareTripServices
{
    Task<List<ShareTripGetDTO>> GetAllAsync(Guid tripId);
    Task<List<ShareTripGetDTO>> GetAllContributorsUser(Guid tripId);
    Task CreateAsync(ShareTripCreateDTO shareTripCreateDTO);
    Task<ShareTripGetDTO> GetByIdAsync(Guid Id);
    Task<bool> AccesTripNote(Guid tripId, string AppUserId);
    Task UpdateAsync(Guid id, ShareTripUpdateDTO shareTripUpdateDTO);
    Task RemoveAsync(Guid id);
    Task RemoveRangeAsync(Guid tripId);
}
