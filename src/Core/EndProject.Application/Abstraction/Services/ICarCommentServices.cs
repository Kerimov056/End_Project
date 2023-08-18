using EndProject.Application.DTOs.CarComment;

namespace EndProject.Application.Abstraction.Services;

public interface ICarCommentServices
{
    Task<List<CarCommentGetDTO>> GetAllAsync(Guid CarId);
    Task CreateAsync(CarCommentCreateDTO carCommentCreateDTO);
    Task<CarCommentGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, CarCommentUpdateDTO carCommentUpdateDTO);
    Task RemoveAsync(Guid id);
}
