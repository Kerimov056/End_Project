using EndProject.Application.DTOs.TripNote;

namespace EndProject.Application.Abstraction.Services;

public interface ITripNoteServices
{
    Task<List<TripNoteGetDTO>> GetAllAsync(Guid TripId);
    Task<List<TripNoteGetDTO>> GetAllMyNote(string AppUserId, Guid TripId);
    Task CreateAsync(TripNoteCreateDTO tripNoteCreateDTO);
    Task<TripNoteGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, TripNoteUpdateDTO tripNoteUpdateDTO);
    Task RemoveAsync(Guid id, string AppUserId);
}
