using EndProject.Application.DTOs.Post;

namespace EndProject.Application.Abstraction.Services;

public interface IPostService
{
    Task<List<PostGetDTO>> GettAllAsync();
    Task AddAsync(PostCreateDTO postCreateDTO);
    Task<PostGetDTO> GetByIdAsync(Guid Id);
    //Task UpdateAsync(Guid Id, PostUpdateDTO);
}
