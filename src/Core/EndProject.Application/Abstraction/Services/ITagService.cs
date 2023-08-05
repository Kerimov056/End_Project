using EndProject.Application.DTOs.Post;
using EndProject.Application.DTOs.Tag;

namespace EndProject.Application.Abstraction.Services;

public interface ITagService
{
    Task<List<TagGetDTO>> GettAllAsync();
    Task AddAsync(TagCreateDTO tagsCreateDTO);
    Task<TagGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid Id, TagUpdateDTO tagsUpdateDTO);
    Task RemoveAsync(Guid Id);
}
