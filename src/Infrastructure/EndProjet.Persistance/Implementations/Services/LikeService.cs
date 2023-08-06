using AutoMapper;
using EndProject.Application.Abstraction.Repositories.IEntityRepository;
using EndProject.Application.Abstraction.Services;
using EndProject.Domain.Entitys;
using EndProject.Domain.Entitys.Identity;
using EndProjet.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace EndProjet.Persistance.Implementations.Services;

public class LikeService : ILikeService
{
    private readonly ILikeReadRepository _likeReadRepository;
    private readonly ILikeWriteRepository _likeWriteRepository;
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public LikeService(ILikeReadRepository likeReadRepository,
                       ILikeWriteRepository likeWriteRepository,
                       AppDbContext appDbContext,
                       IMapper mapper)
    {
        _likeReadRepository = likeReadRepository;
        _likeWriteRepository = likeWriteRepository;
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<int> GetLikeCountForComment(Guid commentId)
    {
        var comment = await _appDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null)
        {
            return 0;
        }
        // Yorumun beyenı sayı cemi.
        return comment.Likes.Count;
    }

    public async Task<List<AppUser>> GetUsersWhoLikedComment(Guid commentId)
    {
        var comment = await _appDbContext.Comments
               .Include(c => c.Likes)
               .ThenInclude(l => l.AppUser)
               .FirstOrDefaultAsync(c => c.Id == commentId);

        if (comment == null)
        {
            return new List<AppUser>();
        }

        // Yorumu beğenen userleri listesi
        return comment.Likes.Select(like => like.AppUser).ToList();
    }

    public async Task LikeCommentAsync(string userId, Guid commentId)
    {
        var comment = await _appDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null)
        {
            return;
        }

        var existingLike = await _appDbContext.Likes.FirstOrDefaultAsync(l => l.CommentsId == commentId && l.AppUserId == userId);
        if (existingLike == null)
        {
            var newLike = new Like
            {
                CommentsId = commentId,
                AppUserId = userId,
                likeSum = 1
            };
            await _appDbContext.Likes.AddAsync(newLike);
        }
        else
        {
            // Daha önce like yapılmış, beğeni sayısını artır.
            existingLike.likeSum++;
        }

        await _appDbContext.SaveChangesAsync();
    }

    public async Task UnlikeCommentAsync(string userId, Guid commentId)
    {
        var comment = await _appDbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null) return;

        var existingLike = await _appDbContext.Likes.FirstOrDefaultAsync(l => l.CommentsId == commentId && l.AppUserId == userId);
        if (existingLike != null)
        {
            if (existingLike.likeSum > 0)
            {
                existingLike.likeSum--;
            }
            _appDbContext.Likes.Remove(existingLike);
        }

        await _appDbContext.SaveChangesAsync();
    }
}
