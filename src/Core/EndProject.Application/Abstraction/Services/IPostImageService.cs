using EndProject.Application.DTOs.Post;

namespace EndProject.Application.Abstraction.Services;

public interface IPostImageService
{
    Task AddAsync(Guid PostId,PostImageCreateDTO postImageCreateDTO);
    Task Update(Guid PostId, PostImageUpdateDTO postImageUpdateDTO);
}
