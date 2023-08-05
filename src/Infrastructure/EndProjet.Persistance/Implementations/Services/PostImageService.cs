using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Post;

namespace EndProjet.Persistance.Implementations.Services;

public class PostImageService : IPostImageService
{
    public Task AddAsync(PostImageCreateDTO postImageCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task Update(Guid PostId, PostImageUpdateDTO postImageUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
