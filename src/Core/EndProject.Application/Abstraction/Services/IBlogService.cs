using EndProject.Application.DTOs.Blog;

namespace EndProject.Application.Abstraction.Services;

public interface IBlogService
{
    Task<List<BlogGetDTO>> GetAllAsync();
    Task CreateAsync(BlogCreateDTO blogCreateDTO);
    Task<BlogGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, BlogUpdateDTO blogUpdateDTO);
    Task RemoveAsync(Guid id);
}
