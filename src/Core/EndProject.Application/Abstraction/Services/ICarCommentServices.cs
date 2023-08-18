using EndProject.Application.DTOs.CarComment;
using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface ICarCommentServices
{
    Task<List<CarCommentGetDTO>> GetAllAsync();
    Task CreateAsync(CarCommentCreateDTO carCommentCreateDTO);
    Task<CarCommentGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, CarCommentUpdateDTO carCommentUpdateDTO);
    Task RemoveAsync(Guid id);
}
