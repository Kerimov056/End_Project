using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Blog;

namespace EndProjet.Persistance.Implementations.Services;

public class BlogService : IBlogService
{
    public Task CreateAsync(BlogCreateDTO blogCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<BlogGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BlogGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, BlogUpdateDTO blogUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
