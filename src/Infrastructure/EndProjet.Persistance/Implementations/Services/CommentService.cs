using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Comments;

namespace EndProjet.Persistance.Implementations.Services;

public class CommentService : ICommentService
{
    public Task AddAsync(CommentCreateDTO commentCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<CommentGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<CommentGetDTO>> GettAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid Id, CommentUpdateDTO commentUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
