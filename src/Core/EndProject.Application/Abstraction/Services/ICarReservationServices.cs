using EndProject.Application.DTOs.CarReservation;
using EndProject.Domain.Entitys;
using EndProject.Domain.Enums.ReservationStatus;

namespace EndProject.Application.Abstraction.Services;

public interface ICarReservationServices
{
    Task<List<CarReservationGetDTO>> GetAllAsync();
    Task<List<CarReservationGetDTO>> IsResevPedingGetAll();
    Task<List<CarReservationGetDTO>> IsResevConfirmedGetAll();
    Task<List<CarReservationGetDTO>> IsResevComplatedGetAll();
    Task<List<CarReservationGetDTO>> IsResevCanceledGetAll();
    Task<List<CarReservationGetDTO>> IsResevNowGetAll();
    Task<List<CarReservationGetDTO>> YouGetAllAsync(string Id);
    Task CreateAsync(CarReservationCreateDTO carReservationCreateDTO);
    Task AllCreateAsync(ReservationFake ReservationFake);
    Task<CarReservationGetDTO> GetByIdAsync(Guid Id);
    Task<CarReservation> GetStatusValue(Guid CarId);
    Task StatusConfirmed(Guid Id);
    Task StatusCompleted(Guid reservId);
    Task StatusCanceled(Guid Id);
    Task StatusNow(Guid Id);
    Task UpdateAsync(Guid id, CarReservationUpdateDTO carReservationUpdateDTO);
    Task RemoveAsync(Guid id);
    Task<int> GetPeddingCountAsync();
    Task<int> GetConfirmedCountAsync();
    Task<int> GetCompletedCountAsync();
    Task<int> GetCanceledCountAsync();
}
