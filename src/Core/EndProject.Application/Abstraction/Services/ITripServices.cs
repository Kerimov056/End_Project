using EndProject.Application.DTOs.Car;
using EndProject.Application.DTOs.Trip;

namespace EndProject.Application.Abstraction.Services;

public interface ITripServices
{
    Task<List<TripGetDTO>> GetAllAsync(string AppUserId);
    Task CreateAsync(TripCreateDTO tripCreateDTO);
    Task<TripGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, TripUpdateDTO tripUpdateDTO);
    Task RemoveAsync(Guid tripId, string AppUserId);

    //MyTripCountAsync
    Task<int> MyTripCountAsync(string AppUserId);

    //TripCar
    Task<CarGetDTO> GetTripByIdAsync(Guid TripId);
}
