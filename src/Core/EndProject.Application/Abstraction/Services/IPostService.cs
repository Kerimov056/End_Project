using EndProject.Application.DTOs.Post;
using EndProject.Domain.Entitys;

namespace EndProject.Application.Abstraction.Services;

public interface IPostService
{
    Task<List<PostGetDTO>> GettAllAsync();
    Task<List<Posts>> GetAllPostsWithDetails();
    Task AddAsync(PostCreateDTO postCreateDTO);
    Task<PostGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid Id, PostCreateDTO postCreateDTO);
    Task RemoveAsync(Guid Id);
}
