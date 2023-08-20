using EndProject.Application.DTOs.OtherCarReservation;

namespace EndProject.Application.Abstraction.Services;

public interface IOtherCarReservationServices
{
    Task<List<OtherCarReservationGetDTO>> GetAllAsync();
    Task<List<OtherCarReservationGetDTO>> IsResevConfirmedGetAll();
    Task<List<OtherCarReservationGetDTO>> IsResevComplatedGetAll();
    Task<List<OtherCarReservationGetDTO>> IsResevCanceledGetAll();
    Task<List<OtherCarReservationGetDTO>> YouGetAllAsync(string Id);
    Task CreateAsync(OtherCarReservationCreateDTO otherCarReservationCreateDTO);
    Task<OtherCarReservationGetDTO> GetByIdAsync(Guid Id);
    Task StatusConfirmed(Guid Id);
    Task StatusCompleted(Guid reservId);
    Task StatusCanceled(Guid Id);
    Task UpdateAsync(Guid id, OtherCarReservationUpdateDTO otherCarReservationUpdateDTO);
    Task RemoveAsync(Guid id);
}
