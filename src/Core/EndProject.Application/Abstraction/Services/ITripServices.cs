using EndProject.Application.DTOs.Trip;

namespace EndProject.Application.Abstraction.Services;

public interface ITripServices
{
    Task<List<TripGetDTO>> GetAllAsync();
    Task CreateAsync(TripCreateDTO tripCreateDTO);
    Task<TripGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, TripUpdateDTO tripUpdateDTO);
    Task RemoveAsync(Guid id);
}
