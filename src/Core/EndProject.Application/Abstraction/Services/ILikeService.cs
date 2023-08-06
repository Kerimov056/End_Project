using EndProject.Application.DTOs.Like;
using EndProject.Application.DTOs.Post;
using EndProject.Domain.Entitys.Identity;

namespace EndProject.Application.Abstraction.Services;

public interface ILikeService
{
    Task LikeCommentAsync(string userId, Guid commentId);
    Task UnlikeCommentAsync(string userId, Guid commentId);
    Task<int> GetLikeCountForComment(Guid commentId);
    Task<List<AppUser>> GetUsersWhoLikedComment(Guid commentId);
}
