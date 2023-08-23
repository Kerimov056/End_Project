using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Like;
using EndProject.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class LikeServices : ILikeServices
{
    private readonly ILikeReadRepository _likeReadRepository;
    private readonly ILikeWriteRepository _likeWriteRepository;
    private readonly ICarCommentReadRepository _carCommentReadRepository;
    private readonly ICarCommentServices _carCommentServices;
    private readonly IMapper _mapper;

    public LikeServices(ILikeReadRepository likeReadRepository,
                        ILikeWriteRepository likeWriteRepository,
                        IMapper mapper,
                        ICarCommentServices carCommentServices)
    {
        _likeReadRepository = likeReadRepository;
        _likeWriteRepository = likeWriteRepository;
        _mapper = mapper;
        _carCommentServices = carCommentServices;
    }

    public async Task CreateAsync(LikeCreateDTO likeCreateDTO)
    {
        var byComment = await _carCommentReadRepository.GetByIdAsync(likeCreateDTO.CarCommentId);
    
        var newLike = new Like
        {
            AppUserId = likeCreateDTO.AppUserId,
            CarCommentId = byComment.Id,
            LikeSum = 1
        };
        byComment.Likes.Add(newLike);
       
        await _likeWriteRepository.AddAsync(newLike);
        await _likeWriteRepository.SavaChangeAsync();
    }

    public async Task<List<LikeGetDTO>> GetAllAsync()
    {
        var likes = await _likeReadRepository.GetAll().ToListAsync();
        return _mapper.Map<List<LikeGetDTO>>(likes);
    }

    public async Task<LikeGetDTO> GetByIdAsync(Guid Id)
    {
        var like = await _likeReadRepository.GetByIdAsync(Id);
        return _mapper.Map<LikeGetDTO>(like);

    }

    public async Task RemoveAsync(Guid id)
    {
        var Bylike = await _likeReadRepository.GetByIdAsync(id);
        _likeWriteRepository.Remove(Bylike);
        await _likeWriteRepository.SavaChangeAsync();
    }

    public async Task UpdateAsync(Guid id, LikeUpdateDTO likeUpdateDTO)
    {
        var byLike = await _likeReadRepository.GetByIdAsync(id);
        _mapper.Map(likeUpdateDTO, byLike);
        if (byLike.LikeSum >= 1)
        {
            byLike.LikeSum = byLike.LikeSum - 1;
        }
        _likeWriteRepository.Update(byLike);
        await _likeWriteRepository.SavaChangeAsync();
    }
}
