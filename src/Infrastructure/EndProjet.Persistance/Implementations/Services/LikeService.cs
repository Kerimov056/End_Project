using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Like;
using EndProject.Application.DTOs.Post;
using EndProject.Domain.Entitys;

namespace EndProjet.Persistance.Implementations.Services;

public class LikeService : ILikeService
{
    private readonly ILikeReadRepository _likeReadRepository;
    private readonly ILikeWriteRepository _likeWriteRepository;
    private readonly IMapper _mapper;

    public LikeService(ILikeReadRepository likeReadRepository,
                       ILikeWriteRepository likeWriteRepository,
                       IMapper mapper)
    {
        _likeReadRepository = likeReadRepository;
        _likeWriteRepository = likeWriteRepository;
        _mapper = mapper;
    }

    public async Task AddAsync(LikeCreateDTO likeCreateDTO)
    {
        var newLike = _mapper.Map<Like>(likeCreateDTO);
        newLike.AppUserId = GetUserId();
        newLike.likeSum = newLike.likeSum + 1;
        await _likeWriteRepository.AddAsync(newLike);
        await _likeWriteRepository.SavaChangeAsync();
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
    private string GetUserId()
    {
        //var user = _contextAccessor.HttpContext.User;
        string userId = "8a244e85-22bd-42a2-aa8e-0d5f93d9bdb4";
        return userId;
    }
}
