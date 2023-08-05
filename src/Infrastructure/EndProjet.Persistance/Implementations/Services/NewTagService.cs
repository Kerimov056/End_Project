using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.NewTag;

namespace EndProjet.Persistance.Implementations.Services;

public class NewTagService : INewTagService
{
    public Task AddAsync(NewTagCreateDTO newTagCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task Update(Guid PostId, NewTagUpdateDTO newTagUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
