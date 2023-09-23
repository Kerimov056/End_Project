using EndProject.Application.DTOs.Trip;

namespace EndProject.Application.Abstraction.Services;

public interface ITripServices
{
    Task<List<TripGetDTO>> GetAllAsync(string AppUserId);
    Task CreateAsync(TripCreateDTO tripCreateDTO);
    Task<TripGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, TripUpdateDTO tripUpdateDTO);
    Task RemoveAsync(TripRemoveDTO tripRemoveDTO);
}
