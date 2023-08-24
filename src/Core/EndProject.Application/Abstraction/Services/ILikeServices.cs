using EndProject.Application.DTOs.Like;

namespace EndProject.Application.Abstraction.Services;

public interface ILikeServices
{
    Task<List<LikeGetDTO>> GetAllAsync();
    Task CreateAsync(LikeCreateDTO likeCreateDTO);
    Task<LikeGetDTO> GetByIdAsync(Guid Id);
    Task RemoveAsync(Guid id);
}
