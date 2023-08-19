using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.BlogImage;

namespace EndProjet.Persistance.Implementations.Services;

public class BlogImageServices : IBlogImageServices
{
    public Task CreateAsync(BlogImageCreateDTO blogImageCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<BlogImageGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BlogImageGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, BlogImageUpdateDTO blogImageUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
