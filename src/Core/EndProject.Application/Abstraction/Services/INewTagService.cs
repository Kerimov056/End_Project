using EndProject.Application.DTOs.NewTag;

namespace EndProject.Application.Abstraction.Services;

public interface INewTagService
{
    Task AddAsync(NewTagCreateDTO newTagCreateDTO);
    Task Update(Guid PostId, NewTagUpdateDTO newTagUpdateDTO);
}
