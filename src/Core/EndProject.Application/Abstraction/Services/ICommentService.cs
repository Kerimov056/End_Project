using EndProject.Application.DTOs.Comments;

namespace EndProject.Application.Abstraction.Services;

public interface ICommentService
{
    Task<List<CommentGetDTO>> GettAllAsync();
    Task AddAsync(CommentCreateDTO commentCreateDTO);
    Task<CommentGetDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(Guid Id, CommentUpdateDTO commentUpdateDTO);
    Task RemoveAsync(Guid Id);
}
