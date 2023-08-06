using EndProject.Application.DTOs.Like;
using EndProject.Application.DTOs.Post;

namespace EndProject.Application.Abstraction.Services;

public interface ILikeService
{
    Task<List<LikeGetDTO>> GettAllAsync();
    Task AddAsync(LikeCreateDTO likeCreateDTO);
    Task<LikeGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid Id, PostUpdateDTO postUpdateDTO);
    Task RemoveAsync(Guid Id);
}
