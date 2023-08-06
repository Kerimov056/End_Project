using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Like;
using EndProject.Application.DTOs.Post;

namespace EndProjet.Persistance.Implementations.Services;

public class LikeService : ILikeService
{
    public Task AddAsync(LikeCreateDTO likeCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<LikeGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<LikeGetDTO>> GettAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid Id, PostUpdateDTO postUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
