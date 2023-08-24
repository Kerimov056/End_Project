using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Application.DTOs.Like;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class LikeServices : ILikeServices
{
    private readonly ILikeReadRepository _likeReadRepository;
    private readonly ILikeWriteRepository _likeWriteRepository;
    private readonly ICarCommentReadRepository _carCommentReadRepository;
    private readonly ICarCommentWriteRepository _carCommentWriteRepository;
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;
    public LikeServices(ILikeReadRepository likeReadRepository,
                        ILikeWriteRepository likeWriteRepository,
                        IMapper mapper,
                        ICarCommentWriteRepository carCommentWriteRepository,
                        AppDbContext appDbContext)
    {
        _likeReadRepository = likeReadRepository;
        _likeWriteRepository = likeWriteRepository;
        _carCommentWriteRepository = carCommentWriteRepository;
        _mapper = mapper;
        _appDbContext = appDbContext;
    }

    public async Task CreateAsync(LikeCreateDTO likeCreateDTO)
    {
        var byLike = await _likeReadRepository
            .GetAll()
            .Where(x => x.AppUserId == likeCreateDTO.AppUserId)
            //.Where(x => x.CarCommentId == likeCreateDTO.CarCommentId)
            .FirstOrDefaultAsync();

        var byComment = await _appDbContext.CarComments.FindAsync(likeCreateDTO.CarCommentId);

        if (byLike is null)
        {
            //var newLike = _mapper.Map<Like>(likeCreateDTO);
            //if (byComment.Likes is null)
            //{
            //    byComment.Likes.Add(newLike);
            //}
            //await _likeWriteRepository.AddAsync(newLike);
        }
        else
        {
            //if (byComment.Likes is not null)
            //{
            //    byComment.Likes.Remove(byLike);
            //}
            _likeWriteRepository.Remove(byLike);
        }

        await _carCommentWriteRepository.SavaChangeAsync();
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

}
