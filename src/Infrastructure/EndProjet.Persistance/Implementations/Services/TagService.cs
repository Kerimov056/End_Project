using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Tag;

namespace EndProjet.Persistance.Implementations.Services;

public class TagService : ITagService
{
    public Task AddAsync(TagCreateDTO tagsCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<TagGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TagGetDTO>> GettAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid Id, TagUpdateDTO tagsUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
