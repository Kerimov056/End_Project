using EndProject.Application.DTOs.BlogImage;
using EndProject.Application.DTOs.Slider;

namespace EndProject.Application.Abstraction.Services;

public interface IBlogImageServices
{
    Task<List<BlogImageGetDTO>> GetAllAsync();
    Task<List<BlogImageDTO>> GetAllBlogIdAsync(Guid blogId);
    Task CreateAsync(BlogImageCreateDTO blogImageCreateDTO);
    Task<BlogImageGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid id, BlogImageUpdateDTO blogImageUpdateDTO);
    Task RemoveAsync(Guid id);
}
