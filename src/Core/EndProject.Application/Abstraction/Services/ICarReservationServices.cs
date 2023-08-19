using EndProject.Application.DTOs.CarReservation;
using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface ICarReservationServices
{
    Task<List<CarReservationGetDTO>> GetAllAsync();
    Task<List<CarReservationGetDTO>> IsResevConfirmedGetAll();
    Task<List<CarReservationGetDTO>> IsResevComplatedGetAll();
    Task<List<CarReservationGetDTO>> IsResevCanceledGetAll();
    Task<List<CarReservationGetDTO>> YouGetAllAsync(string Id);
    Task CreateAsync(CarReservationCreateDTO carReservationCreateDTO);
    Task<CarReservationGetDTO> GetByIdAsync(Guid Id);
    Task StatusConfirmed(Guid Id);
    Task StatusCanceled(Guid Id);
    Task UpdateAsync(Guid id, CarReservationUpdateDTO carReservationUpdateDTO);
    Task RemoveAsync(Guid id);
}
