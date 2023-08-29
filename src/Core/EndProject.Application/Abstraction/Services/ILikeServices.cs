using EndProject.Application.DTOs.Like;
using System.ComponentModel.DataAnnotations;

namespace EndProject.Application.Abstraction.Services;

public interface ILikeServices
{
    Task<List<LikeGetDTO>> GetAllAsync();
    Task CreateAsync(string AppUserId, Guid CarCommentId);
    Task<LikeGetDTO> GetByIdAsync(Guid Id);
    Task RemoveAsync(Guid id);
}
