using EndProject.Application.DTOs.Like;
using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface ILikeServices
{
    Task<List<LikeGetDTO>> GetAllAsync();
    Task CreateAsync(LikeCreateDTO likeCreateDTO);
    Task<LikeGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, LikeUpdateDTO likeUpdateDTO);
    Task RemoveAsync(Guid id);
}
