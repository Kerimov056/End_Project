using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Like;

namespace EndProjet.Persistance.Implementations.Services;

public class LikeServices : ILikeServices
{
    private readonly ILikeReadRepository _likeReadRepository;
    private readonly ILikeWriteRepository _likeWriteRepository;
    private readonly IMapper _mapper;

    public LikeServices(ILikeReadRepository likeReadRepository,
                        ILikeWriteRepository likeWriteRepository,
                        IMapper mapper)
    {
        _likeReadRepository = likeReadRepository;
        _likeWriteRepository = likeWriteRepository;
        _mapper = mapper;
    }

    public Task CreateAsync(LikeCreateDTO likeCreateDTO)
    {
        throw new NotImplementedException();
    }

    public Task<List<LikeGetDTO>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<LikeGetDTO> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid id, LikeUpdateDTO likeUpdateDTO)
    {
        throw new NotImplementedException();
    }
}
